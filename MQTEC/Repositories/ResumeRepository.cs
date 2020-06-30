using MQTEC.Data;
using MQTEC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MQTEC.Repositories
{
    public class ResumeRepository
    {
        private readonly MQTECDbContext _context;
        public ResumeRepository(MQTECDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetTableAsync()
        {
            return await _context.Products.Include(p => p.Category)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
