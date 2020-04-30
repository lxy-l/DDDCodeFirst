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
    public class UserProfileMap: EntityTypeConfiguration<UserProfile>
    {
        public UserProfileMap()
        {
            HasKey(t => t.Id);
            Property(t => t.Name).IsRequired()
                               .HasMaxLength(100)
                               .HasColumnType("nvarchar");
            Property(t => t.Address).HasColumnType("nvarchar");
            Property(t => t.ModifiedDate).IsRequired();
            Property(t => t.AddedDate).IsRequired();
            ToTable("UserProfiles");

            // 关系 : 1对1
            HasRequired(t => t.User).WithRequiredDependent(u => u.UserProfile);
        }
    }
}
