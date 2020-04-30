namespace Entities
{
    /// <summary>
    /// 业务模型：用户明细信息
    /// </summary>
    public class UserProfile : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }

        // 导航属性：
        // 一个会员明细对应一个会员基础信息
        public virtual User User { get; set; }
    }
}