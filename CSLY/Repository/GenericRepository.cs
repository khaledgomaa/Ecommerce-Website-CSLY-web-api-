using CSLY.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace CSLY.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected ApplicationDbContext dbContext;

        public GenericRepository(ApplicationDbContext DBEntity)
        {
            dbContext = DBEntity;
        }

        public void Add(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbContext.Set<TEntity>().ToList();
        }

        public int GetAllRecordsCount()
        {
            return dbContext.Set<TEntity>().Count();
        }

        public TEntity GetFirstOrDefault(int recordId)
        {
           
            return dbContext.Set<TEntity>().Find(recordId);
        }

        public TEntity GetFirstOrDefaultByParam(Expression<Func<TEntity, bool>> wherePredict)
        {
            return dbContext.Set<TEntity>().Where(wherePredict).FirstOrDefault();
        }

        public IEnumerable<TEntity> GetListParameter(Expression<Func<TEntity, bool>> wherePredict)
        {
            return dbContext.Set<TEntity>().Where(wherePredict).ToList();
        }

        public IEnumerable<TEntity> GetResultBySqlProcedure(string sql, params object[] parameters)
        {
            if (parameters != null)
            {
                return dbContext.Database.SqlQuery<TEntity>(sql, parameters).ToList();
            }
            return dbContext.Database.SqlQuery<TEntity>(sql).ToList();
        }

        public void InActiveAndDeleteByWhereClause(Expression<Func<TEntity, bool>> wherePredict, Action<TEntity> forEachPredict)
        {
            dbContext.Set<TEntity>().Where(wherePredict).ToList().ForEach(forEachPredict);
        }

        public void Remove(TEntity entity)
        {
            dbContext.Entry(entity).State = EntityState.Deleted;
        }

        public void RemoveByWhereClause(Func<TEntity, bool> wherePredict)
        {
            TEntity entity = dbContext.Set<TEntity>().Where(wherePredict).FirstOrDefault();
            dbContext.Set<TEntity>().Remove(entity);
        }

        public void RemoveByWhereClauseRange(Expression<Func<TEntity, bool>> wherePredict)
        {
            List<TEntity> entity = dbContext.Set<TEntity>().Where(wherePredict).ToList();
            foreach (var ent in entity)
            {
                dbContext.Set<TEntity>().Remove(ent);
            }
        }

        public IEnumerable<TEntity> GetRecordsToShow(int pageNo, int size, int currentPage, Expression<Func<TEntity, bool>> wherePredict, Expression<Func<TEntity, int>> orderByPredict)
        {
            if (wherePredict != null)
            {
                return dbContext.Set<TEntity>().OrderBy(orderByPredict).Where(wherePredict).ToList();
            }
            return dbContext.Set<TEntity>().OrderBy(orderByPredict).ToList();
        }

        public IEnumerable<TEntity> GetAllInclude(Expression<Func<TEntity, object>> includes)
        {
            return dbContext.Set<TEntity>().Include(includes).ToList();
        }

        //public IQueryable<TEntity> IncludeMultiple<TEntity>(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
        //{
        //    {
        //        if (includes != null)
        //        {
        //            query = includes.Aggregate(query,
        //                      (current, include) => current.Include(include));
        //        }

        //        return query;
        //    }
        //}
    }
}