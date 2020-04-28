using KofCWebsite.Core.Entities.KofC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KofCWebSite.Core.Interfaces
{
    public interface IKofCRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> Items { get; }
        Task<T> GetEntityByIdAsync(int Id);
        void Insert(T entity);
        void DeleteById(int Id);
        void DeActivateById(int Id);
        void Update(T entity);
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
