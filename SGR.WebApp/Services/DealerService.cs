using SGR.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBBlazorApp.Interfaces;

namespace TBBlazorApp.Services
{
    public class DealerService : ISGRService<Dealer>
    {
        private SGIDbContext _db;

        public DealerService(SGIDbContext db)
        {
            _db = db;
        }

        public Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                Dealer entity = _db.Dealers.Find(Id);
                entity.Deleted = DateTime.Now;
                _db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _db.SaveChangesAsync();
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
                throw;
            }
        }

        public Task<List<Dealer>> GetAllAsync()
        {
            var list = _db.Dealers.Where(o => o.Deleted == null).ToList();
            return Task.FromResult(list);
        }

        public Task<Dealer> GetByIdAsync(Guid Id)
        {
            var entity = _db.Dealers.Find(Id);
            return Task.FromResult(entity);
        }

        public Task<Dealer> SaveAsync(Dealer obj)
        {
            obj.Created = DateTime.Now;
            obj.Id = Guid.NewGuid();
            _db.Dealers.AddAsync(obj);
            _db.SaveChangesAsync();
            return Task.FromResult(obj);
        }

        public Task<Dealer> UpdateAsync(Guid Id, Dealer obj)
        {
            obj.Modified = DateTime.Now;
            _db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChangesAsync();
            return Task.FromResult(obj);
        }
    }
}
