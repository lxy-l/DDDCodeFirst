using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// DTO（数据传值用）
    /// 用于SQL多表查询的类，不参与建表
    /// </summary>
    public class UserExt : BaseEntity
    {
        // 将User和UserProfile两个实体的数据集中在此类中显示
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

    }
}
