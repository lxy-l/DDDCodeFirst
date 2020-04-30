using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    /// <summary>
    /// 会员管理的业务功能接口
    /// </summary>
    public interface IUserService : IDisposable
    {
        /// <summary>
        /// 获取会员
        /// </summary>
        /// <returns></returns>
        IEnumerable<User> GetUsers();
        /// <summary>
        /// 根据会员ID查找会员
        /// </summary>
        /// <param name="id">会员ID</param>
        /// <returns></returns>
        User GetUserById(int id);
        /// <summary>
        /// 根据会员姓名查找会员
        /// </summary>
        /// <param name="name">姓名</param>
        /// <returns></returns>
        IEnumerable<User> GetUserByName(string name);
        /// <summary>
        /// 新增会员
        /// </summary>
        /// <param name="user"></param>
        void InsertUser(User user);
        /// <summary>
        /// 更新会员
        /// </summary>
        /// <param name="user"></param>
        void UpdateUser(User user);
        /// <summary>
        /// 删除会员
        /// </summary>
        /// <param name="user"></param>
        void DeleteUser(User user);

        /// <summary>
        /// 修改会员（扩展版）
        /// </summary>
        /// <param name="id">会员id</param>
        /// <param name="user">会员模型对象</param>
        /// <returns></returns>
        int UpdateUserExt(int id, User user);

        /// <summary>
        /// 删除会员（扩展版）
        /// </summary>
        /// <param name="id">会员ID</param>
        /// <returns></returns>
        int DeleteUserExt(int id);

        /// <summary>
        /// 修改会员信息（EF的并发处理版）
        /// </summary>
        /// <param name="user">User业务模型</param>
        /// <param name="rowVersion">时间戳</param>
        void UpdateUserRowVserion(User user, byte[] rowVersion);

        /// <summary>
        /// 获取用户详情（SQL语句版）
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserExt> ListUser();
    }
}
