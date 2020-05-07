using CSLY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSLY.Repository
{
    public class GenericUnitofWork : IDisposable
    {
        protected ApplicationDbContext DbEntity = new ApplicationDbContext();
        

        public IRepository<TEntityType> GetRepositoryInstance<TEntityType>() where TEntityType : class
        {
            return new GenericRepository<TEntityType>(DbEntity);
        }

        public void Complete()
        {
            DbEntity.SaveChanges();
        }
        
        public void Dispose()
        {
            DbEntity.Dispose();
        }
    }
}