using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestApp_17_06_2022.Data.Entities;
using TestApp_17_06_2022.Data.Interfaces;
using TestApp_17_06_2022.Models;

namespace TestApp_17_06_2022.Data.Classes
{
    public class ProductRepository : IRepository<ProductFormViewModel>
    {
        private readonly IAppDbContext _db;
        
        public ProductRepository(IAppDbContext db)
        {
            _db = db;
        }
        
        public void Dispose()
        {
            _db?.Dispose();
        }

        public async Task<List<ProductFormViewModel>> GetAll()
        {
            var model = await _db.Products.Select(s => new ProductFormViewModel()
            {
                Id = s.Id,
                Name = s.Name
            }).ToListAsync();
            
            return model;
        }

        public async Task<ProductFormViewModel> Get(long id)
        {
            var entity = await _db.Products.FirstOrDefaultAsync(s => s.Id == id);

            return new ProductFormViewModel() {Id = entity.Id, Name = entity.Name};
        }

        public async Task Create(ProductFormViewModel item)
        {
            await _db.Products.AddAsync(new Product
            {
                Name = item.Name
            });
            await _db.SaveChangesAsync();
        }

        public async Task Update(ProductFormViewModel item)
        {
            var entity = await _db.Products.FirstOrDefaultAsync(s => s.Id == item.Id);
            entity.Name = item.Name;
            _db.Products.Update(entity);
            await _db.SaveChangesAsync();
        }

        public void Delete(long id)
        {
            var entity = _db.Products.FirstOrDefault(s => s.Id == id);
            if (entity != null)
            {
                _db.Products.Remove(entity);
                _db.SaveChangesAsync();
            }
        }
    }
}