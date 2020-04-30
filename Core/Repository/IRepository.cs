using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    /// <summary>
    /// 通用仓储访问接口
    /// </summary>
    public interface IRepository<T> where T: BaseEntity
    {
        /// <summary>
        /// 查询所有记录
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> Get();
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        void Insert(T model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        void Delete(T model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        void Delete(object id);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        void Update(T model);
        /// <summary>
        ///  条件通用查询
        /// </summary>
        /// <param name="filter">表达式树，实现动态多条件查询</param>
        /// <param name="orderby">排序方法</param>
        /// <param name="prop">预加载（输入多表的名称，如："Student,Score"）</param>
        /// <returns></returns>
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
                           Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
                           string prop = "");

        /// <summary>
        /// 获取数据表对象
        /// </summary>
        DbSet<T> GetDbSet { get; }

        /// <summary>
        /// 对数据并发的处理
        /// </summary>
        /// <param name="model">对象</param>
        /// <param name="rowVersion">时间戳</param>
        void UpdateRowVersion(T model, byte[] rowVersion);
    }
}
