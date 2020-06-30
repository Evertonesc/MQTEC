using MQTEC.Data;
using MQTEC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MQTEC.Repositories
{
    public class ProductRepository
    {
        private readonly MQTECDbContext _context;

        public ProductRepository(MQTECDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetTableAsync()
        {
            return await _context.Products.Include(p => p.Category)
                .AsNoTracking()
                .ToListAsync();
        }

        public void Save(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
        }

        public async Task Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public Product Edit(int? id)
        {
            return _context.Products.Find(id);
        }

        public async Task ConfirmEdit(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
        }

        public bool Exists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        public IEnumerable GetCategory()
        {
            return new SelectList(_context.Categories, "Id", "Title");
        }

        public IEnumerable GetCategoryEdit(Product product)
        {
            return new SelectList(_context.Categories, "Id", "Title", product.CategoryId);
        }
    }
}
