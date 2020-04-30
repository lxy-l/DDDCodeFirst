using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 导入EF包：
using System.Data.Entity.ModelConfiguration;
using Entities;

namespace Core.Mapping
{
    /// <summary>
    /// 映射类：业务模型与表的映射
    /// </summary>
    public class UserMap:EntityTypeConfiguration<User>
    {
        // 根据User业务模型创建用户数据表
        public UserMap()
        {
            // 主键
            HasKey(t => t.Id);
            // 其它字段
            Property(t => t.UserName).IsRequired();     // 非空
            Property(t => t.Email).IsRequired();
            Property(t => t.Password).IsRequired();
            Property(t => t.ModifiedDate).IsRequired();
            Property(t => t.AddedDate).IsRequired();
            Property(t => t.RowVersion).IsRowVersion(); // 时间戳
            // 表名
            ToTable("Users");
        }
    }
}
