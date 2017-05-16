using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBuy.IDAL
{
    /*
     * 基础类操作接口（包含查询，分页查询，增，删，改操作）
     * author:xiaoshan
     * date:2017/5/7
     */
    public interface IBaseDal<T> where T : class,new()
    {
        IQueryable<T> LoadEntityes(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda);
        IQueryable<T> LoadPageEntites<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderByLambda, bool isAsc);
        bool DeleteEntity(T entity);
        bool EditEntity(T entity);
        T AddEntity(T entity);
    }
}
