using KofCWebSite.Core.Interfaces;
using KofCWebSite.Data.KofC;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KofCWebSite.Data.Repositories
{
    public class KofCRepository<T> : IKofCRepository<T> where T : class
    {
        private KofCDbContext _db = null;
        private DbSet<T> table = null;
       
        public KofCRepository(KofCDbContext dbContext)
        {
            _db = dbContext;
            table = _db.Set<T>();
        }

        public void DeleteById(int Id)
        {
            T entityObj = _db.Find<T>(Id);
            _db.Remove(entityObj);
            SaveChanges();
        }

        public void DeActivateById(int Id)
        {
            T entityObj = _db.Find<T>(Id);
            var statusProp = entityObj.GetType().GetProperty("Status");

            if (statusProp == null)
            {
                throw new DbUpdateException("Entity does not have a column named 'Status'");
            }

            statusProp.SetValue(statusProp, 'X');
            SaveChanges();
        }

        public IQueryable<T> GetAll() 
        {
            return table;
        }

        public IQueryable<T> Items => table;

        public async Task<T> GetEntityByIdAsync(int Id)
        {
            table = _db.Set<T>();
            return await table.FindAsync(Id);
        }

        public void Insert(T entity)
        {
            table.Add(entity);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }


        public void Update(T entity)
        {
            table.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
        }
    }
}
