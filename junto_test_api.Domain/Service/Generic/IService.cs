using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace junto_test_api.Domain.Service
{
    public interface IService<TViewModel, TEntity>
    {
        IEnumerable<TViewModel> GetAll();
        int Add(TViewModel obj);
        int Update(TViewModel obj);
        int Remove(int id);
        TViewModel GetOne(int id);
        IEnumerable<TViewModel> Get(Expression<Func<TEntity, bool>> predicate);
    }
}
