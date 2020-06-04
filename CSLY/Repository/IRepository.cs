using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace CSLY.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAllInclude(Expression<Func<TEntity, object>> includes);
        int GetAllRecordsCount();
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entity);
        TEntity GetFirstOrDefault(int recordId);
        void Remove(TEntity entity);
        void RemoveByWhereClause(Func<TEntity, bool> wherePredict);
        void RemoveByWhereClauseRange(Expression<Func<TEntity, bool>> wherePredict);
        void InActiveAndDeleteByWhereClause(Expression<Func<TEntity, bool>> wherePredict, Action<TEntity> forEachPredict);
        TEntity GetFirstOrDefaultByParam(Expression<Func<TEntity, bool>> wherePredict);
        IEnumerable<TEntity> GetListParameter(Expression<Func<TEntity, bool>> wherePredict);
        IEnumerable<TEntity> GetResultBySqlProcedure(string sql, params object[] parameters);
        IEnumerable<TEntity> GetRecordsToShow(int pageNo, int size, int currentPage, Expression<Func<TEntity, bool>> wherePredict, Expression<Func<TEntity, int>> orderByPredict);
    }
}