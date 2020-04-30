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
    /// 通用仓储的实现类（EF版本）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        // 上下文对象
        IocDbContext db;
        // 表对象
        DbSet<T> dbSet;

        // Ninject的构造函数注入
        public Repository(IocDbContext db)
        {
            this.db = db;
            this.dbSet = db.Set<T>();
        }

        public void Delete(object id)
        {
            // 根据主键查找数据实体对象
            T t = dbSet.Find(id);
            // 产生删除SQL语句
            dbSet.Remove(t);
        }

        public void Delete(T model)
        {
            // 判断model对象是否是游离状态
            // 如果是，则附加到上下文对象
            if(db.Entry(model).State == EntityState.Detached)
            {
                dbSet.Attach(model);
            }
            dbSet.Remove(model);
        }

        public void Insert(T model)
        {
            // 产生insert语句
            dbSet.Add(model);
        }

        public void Update(T model)
        {
            // 将对象附加到上下文
            dbSet.Attach(model);
            // 产生update语句
            db.Entry(model).State = EntityState.Modified;
        }

        public IEnumerable<T> Get()
        {
            return dbSet.ToList();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string prop = "")
        {
            IQueryable<T> query = dbSet;

            // 条件查询
            if(filter!=null)
            {
                query = query.Where(filter);
            }
            // 多表查询, prop根据逗号拆分出表名
            foreach (var item in prop.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
            {
                // EF预加载
                query = query.Include(item);
            }
            // 排序
            if(orderby!=null)
            {
                return orderby(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public DbSet<T> GetDbSet
        {
            get { return this.dbSet; }
        }

        public void UpdateRowVersion(T model, byte[] rowVersion)
        {
            // 使用对象附加到EF上下文
            dbSet.Attach(model);
            // 设置模型的RowVersion的值必须与参数rowVersion对应 （不对应的时候，EF自动抛出并发异常作为提示）
            db.Entry(model).OriginalValues["RowVersion"] = rowVersion;
            // 设置模型对象的状态为修改
            db.Entry(model).State = EntityState.Modified;

        }
    }
}
