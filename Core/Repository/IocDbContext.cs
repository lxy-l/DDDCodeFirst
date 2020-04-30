using Core.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    /// <summary>
    /// 数据上下文类
    /// </summary>
    public class IocDbContext : DbContext
    {
        // 连接串名字：IocDB
        public IocDbContext() : base("name=IocDB")
        {
            // 建库模式
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<IocDbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // 表
            // 由于表结构通过映射类配置，使用下面的配置方法调用映射类创建表
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserProfileMap());
        }
       
    }
}
