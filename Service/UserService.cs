using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Repository;
using Core.UnitofWork;
using Entities;
using EntityFramework.Extensions;

namespace Service
{
    /// <summary>
    /// 会员管理的业务功能实现（EF版本）
    /// </summary>
    public class UserService : IUserService
    {
        // 工作单元对象
        IUnitofWork unit;
        // 当前实现类需要用的仓储对象
        IRepository<User> userRep;
        IRepository<UserProfile> userProfileRep;

        //Ninject通过构造函数注入
        public UserService(IUnitofWork unit)
        {
            this.unit = unit;
            userRep = unit.Repository<User>();
            userProfileRep = unit.Repository<UserProfile>();
        }

        public IEnumerable<User> GetUsers()
        {
            return userRep.Get();
        }

        public User GetUserById(int id)
        {
            return userRep.Get(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<User> GetUserByName(string name)
        {
            return userRep.Get(x => x.UserName.Contains(name));
            // 如果要排序查询:
            // return userRep.Get(orderby: x => x.OrderBy(u => u.UserName));
        }
        public void InsertUser(User user)
        {
            userRep.Insert(user);
            unit.Commit();
        }
        public void UpdateUser(User user)
        {
            userRep.Update(user);
            userProfileRep.Update(user.UserProfile);
            unit.Commit();
        }
        public void DeleteUser(User user)
        {
            // 级联删除
            userProfileRep.Delete(user.UserProfile.Id);
            userRep.Delete(user);
            unit.Commit();
        }
        public void Dispose()
        {
            unit.Dispose();
        }

        public int UpdateUserExt(int id, User user)
        {
            try
            {
                // 使用EF的扩展包实现更新操作的优化
                // 通过主键直接更新对象，而不是EF默认先查找对象再更新
                userRep.GetDbSet.Where(x => x.Id == id).Update(
                    x => new User
                    {
                        Email = user.Email,
                        ModifiedDate = DateTime.Now,
                        AddedDate = user.AddedDate,
                        UserName = user.UserName,
                        Password = user.Password
                    }
               );

                userProfileRep.GetDbSet.Where(x => x.Id == id).Update(
                    x => new UserProfile
                    {
                        Name = user.UserProfile.Name,
                        Address = user.UserProfile.Address,
                        ModifiedDate = DateTime.Now,
                        AddedDate = user.UserProfile.AddedDate
                    }
                );
                return 1;
            }
            catch (DataException ex)
            {
                return 0;
            }
        }

        public int DeleteUserExt(int id)
        {
            // 此处的删除不同EF默认的删除
            // 减少了一次先查询的操作，直接根据主键id删除
            try
            {
                // 先删除从表：UserProfile
                userProfileRep.GetDbSet.Where(x => x.Id == id).Delete();
                // 再删除主表：User
                userRep.GetDbSet.Where(x => x.Id == id).Delete();

                return 1;
            }
            catch(DataException ex)
            {
                return 0;
            }
        }


        public void UpdateUserRowVserion(User user, byte[] rowVersion)
        {
            // 先更新主表(时间戳在主表上面)
            userRep.UpdateRowVersion(user, rowVersion);
            // 再更新从表 (从表没有时间戳，直接用普通的Update)
            userProfileRep.Update(user.UserProfile);
            // 提交
            unit.Commit();
        }

        public IEnumerable<UserExt> ListUser()
        {
            string sql = @"select a.id, username, name, address, email, a.AddedDate, a.modifiedDate
                            from users a, UserProfiles b 
                            where a.id = b.id";
            return unit.GetDbContext.Database.SqlQuery<UserExt>(sql);
        }

    }
}
