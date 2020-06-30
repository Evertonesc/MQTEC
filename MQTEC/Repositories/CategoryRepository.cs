using Microsoft.EntityFrameworkCore;
using MQTEC.Data;
using MQTEC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MQTEC.Repositories
{
    public class CategoryRepository
    {
        private readonly MQTECDbContext _context;
        public CategoryRepository(MQTECDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetTableAsync()
        {
            return await _context.Categories
                .AsNoTracking()
                .ToListAsync();
        }

        public void Save(Category category)
        {
            _context.Add(category);
            _context.SaveChanges();
        }

        public async Task Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public Category Edit(int? id)
        {
            return _context.Categories.Find(id);
        }

        public async Task ConfirmEdit(Category category)
        {
            _context.Update(category);
            await _context.SaveChangesAsync();
        }

        public bool Exists(int id)
        {
            return  _context.Categories.Any(e => e.Id == id);
        }
    }
}
