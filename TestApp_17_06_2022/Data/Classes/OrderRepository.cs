using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp_17_06_2022.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestApp_17_06_2022.Data.Entities;
using TestApp_17_06_2022.Data.Interfaces;

namespace TestApp_17_06_2022.Data.Classes
{
    public class OrderRepository : IRepository<OrderFormViewModel>
    {
        private readonly IAppDbContext _db;
        private readonly AppProperties Properties;

        public OrderRepository(IAppDbContext db, AppProperties props)
        {
            _db = db;
            Properties = props;
        }
        
        public void Dispose()
        {
            _db?.Dispose();
        }

        public async Task<List<OrderFormViewModel>> GetAll()
        {
            if (_db.Orders.Any())
            {
                var model = await _db.Orders.Select(s => new OrderFormViewModel()
                {
                    Id = s.Id,
                    Name = s.Product.Name ?? string.Empty,
                    Count = s.Count,
                    Price = s.Price,
                    ProductId = s.ProductId,
                    TotalPriceWithVAT = ((decimal)s.Count * s.Price) * (1 + Properties.VatValue)
                }).ToListAsync();

                return model;
            }

            return new List<OrderFormViewModel>();
        }

        public async Task<OrderFormViewModel> Get(long id)
        {
            var entity = await _db.Orders.Where(s => s.Id == id).Include(s=>s.Product).FirstOrDefaultAsync();

            if (entity == null)
                return new OrderFormViewModel();
            
            return new OrderFormViewModel()
            {
                Id = entity.Id, 
                Name = entity.Product.Name ?? string.Empty, 
                Count = entity.Count, 
                Price = entity.Price
            };
        }

        public async Task Create(OrderFormViewModel item)
        {
            await _db.Orders.AddAsync(new Order
            {
                Count = item.Count,
                Price = item.Price,
                ProductId = item.ProductId
            });
            await _db.SaveChangesAsync();
        }

        public async Task Update(OrderFormViewModel item)
        {
            var entity = await _db.Orders.FirstOrDefaultAsync(s => s.Id == item.Id);
            entity.Count = item.Count;
            entity.Price = item.Price;
            entity.ProductId = item.ProductId;
            _db.Orders.Update(entity);
            await _db.SaveChangesAsync();
        }

        public void Delete(long id)
        {
            var entity = _db.Orders.FirstOrDefault(s => s.Id == id);
            if (entity != null)
            {
                _db.Orders.Remove(entity);
                _db.SaveChangesAsync();
            }
        }
    }
}