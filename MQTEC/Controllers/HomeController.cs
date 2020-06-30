using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MQTEC.Models;
using System.Linq;
using MQTEC.Repositories;

namespace MQTEC.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeRepository _repository;
        public HomeController(HomeRepository repository)
        {
            _repository = repository;
        }


        public IActionResult Index()
        {
            //List<string> chart = _repository.GetTableChart();
            //ViewBag.chart = chart[0];
            return View();
        }

        [HttpPost]
        public JsonResult NovoGrafico()
        {
            List<object> iDados = _repository.GetTableChart();

            return Json(iDados);
            //Criando dados de exemplo
            //DataTable dt = new DataTable();
            //dt.Columns.Add("Vendas", System.Type.GetType("System.String"));
            //dt.Columns.Add("Unidades", System.Type.GetType("System.Int32"));
            //DataRow dr = dt.NewRow();
            //dr["Vendas"] = "Chevrolet Onix";
            //dr["Unidades"] = 171;
            //dt.Rows.Add(dr);
            //dr = dt.NewRow();
            //dr["Vendas"] = "Huynday HB20";
            //dr["Unidades"] = 96;
            //dt.Rows.Add(dr);
            //dr = dt.NewRow();
            //dr["Vendas"] = "Ford Ka(Hatch)";
            //dr["Unidades"] = 87;
            //dt.Rows.Add(dr);
            //dr = dt.NewRow();
            //dr["Vendas"] = "WolksVagem Gol";
            //dr["Unidades"] = 67;
            //dt.Rows.Add(dr);
            //dr = dt.NewRow();
            //dr["Vendas"] = "Renaul Sandero";
            //dr["Unidades"] = 63;
            //dt.Rows.Add(dr);
            //Percorrendo e extraindo cada DataColumn para List<Object>
            //foreach (DataColumn dc in dt.Columns)
            //{
            //    List<object> x = new List<object>();
            //    x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
            //    iDados.Add(x);
            //}
            //Dados retornados no formato JSON

        }

        [HttpPost]
        public JsonResult Chart()
        {
            List<object> charts = _repository.GetTableChart();
            return Json(charts, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
