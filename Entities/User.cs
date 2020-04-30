using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// 业务模型：会员基础信息
    /// </summary>
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] RowVersion { get; set; }

        // public int UserProfileId { get; set; }

        // 导航属性:
        // 一个会员对应一个明细
        public virtual UserProfile UserProfile { get; set; }
    }
}
