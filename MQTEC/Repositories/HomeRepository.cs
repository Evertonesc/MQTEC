using System;
using System.Collections.Generic;
using MQTEC.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MQTEC.Repositories
{
    public class HomeRepository
    {
        private readonly MQTECDbContext _context;

        public HomeRepository(MQTECDbContext context)
        {
            _context = context;
        }

        public string x { get; set; }

        public List<object> GetTableChart()
        {
            string strConn = "Server=DESKTOP-NPSHI85\\EVERTONESC;Database=MQTEC;User Id=react;Password=react";
            var cmdQuery = "SELECT C.TITLE, P.QUANTITY FROM Products P INNER JOIN Categories C ON (C.Id = P.CategoryId)";
            List<object> iDados = new List<object>();
            using (SqlConnection sqlConn = new SqlConnection(strConn))
            {
                sqlConn.Open();
                //string result = "[['Category', Quantity],";
                List<object> chartList = new List<object>();
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand(cmdQuery, sqlConn);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dt.Load(dr);
                }

                foreach (DataColumn dc in dt.Columns)
                {
                    List<object> x = new List<object>();
                    x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                    iDados.Add(x);
                }
                return iDados;
            }
        }
    }
}
