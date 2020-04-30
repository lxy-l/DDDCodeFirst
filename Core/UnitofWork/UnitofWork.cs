using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Repository;
using Entities;

namespace Core.UnitofWork
{
    public class UnitofWork : IUnitofWork
    {
        // 上下文对象
        DbContext db;

        // Ninject通过构造函数注入
        public UnitofWork(DbContext db)
        {
            this.db = db;

            // 全局禁用延迟加载
            // db.Configuration.LazyLoadingEnabled = false;
        }

        // 数据资源的释放
        public void Dispose()
        {
            // 已经手动释放资源
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        bool disposed = false;
        private void Dispose(bool v)
        {
            if(!disposed && v)
            {
                db.Dispose();
            }
            disposed = true;
        }

        public void Commit()
        {
            db.SaveChanges();
        }

        // 通过泛型反射动态初始化所有仓库对象
        // 初始化的结果保存可以到缓存中以提高初始化效率
        
        // 键值对缓存对象
        Dictionary<string, object> repositories;

        public Repository<T> Repository<T>() where T : BaseEntity
        {
            // 获取缓存对象
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }
            // 从缓存对象中获取仓储对象
            var type = typeof(T).Name;
            if(!repositories.ContainsKey(type))
            {
                // 如果仓储对象不存在，使用反射初始化
                var repositoryType = typeof(Repository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), db);
                // 把仓储对象加入到缓存
                repositories.Add(type, repositoryInstance);
            }
            // 从缓存中获取仓储对象
            return repositories[type] as Repository<T>;
        }

        public DbContext GetDbContext => this.db;
    }
}
