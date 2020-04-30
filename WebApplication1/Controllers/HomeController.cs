using AutoMapper;
using Entities;
using Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        
        IUserService userService;

        // Ninject注入
        public HomeController(IUserService service)
        {
            userService = service;
        }

        // 资源释放
        protected override void Dispose(bool disposing)
        {
            userService.Dispose();
            base.Dispose(disposing);
        }

        // 显示首页
        public ActionResult Index()
        {
            var users = userService.ListUser();
            return View(users);
        }


        // 下面使用模型对象映射框架
        //==============================================================================================

        // 查看会员明细:
        public ActionResult AutoMapperTest1()
        {
            // 业务模型User
            User user = new User
            {
                Id = 1,
                UserName = "zhangsan",
                Email = "123@qq.com",
                AddedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Password = "123"
            };
            // 业务模型UserProfile
            UserProfile userProfile = new UserProfile
            {
                Name = "张三",
                Address = "张三的住址",
                AddedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
            user.UserProfile = userProfile;

            // 自动映射（自动从User对象转换为UserModel对象）
            var obj = Mapper.Map<UserModel>(user);

            // 将自动映射出来的UserModel对象传递到视图
            return View(obj);
        }

        // 从数据库读取会员及其明细信息 （查询单个对象）
        public ActionResult AutoMapperTest2()
        {
            // 查询id=1的会员记录
            var userEntity = userService.GetUserById(1);
            // 自动映射（自动从User对象转换为UserModel对象）
            var userModel = Mapper.Map<UserModel>(userEntity);
            // 强类型视图传值
            return View(userModel);
        }

        // 从数据库读取会员及其明细 列表信息 （查询集合对象）
        public ActionResult AutoMapperTest3()
        {
            // 查询所有学生记录列表
            var stus = userService.GetUsers();
            // 自动映射（自动从 IEnumerable<User>对象转换为IEnumerable<UserModel>对象）
            var userModel = Mapper.Map<IEnumerable<UserModel>>(stus);
            // 强类型视图传值
            return View(userModel);
        }

        // 显示添加或编辑视图
        public ActionResult CreateEditUser(int? id)
        {
            // 视图模型对象传值
            UserModel model = new UserModel();

            // 判断参数id是否有值
            if (id.HasValue && id != 0)
            {
                // 通过Service类获取数据库的值
                var userEntity = userService.GetUserById(id.Value);

                if (userEntity != null)
                {
                    // 自动映射
                    model = Mapper.Map<UserModel>(userEntity);
                }

                // 编辑 ： 需要强类型传值
                return View(model);
            }
            // 添加
            return View(model);
        }

        // 提交会员表单 ： 使用添加或编辑
        [HttpPost]
        public ActionResult CreateEditUser(UserModel model)
        {
            // 自动映射 （将UserModel对象映射为User对象）
            var userEntity = Mapper.Map<User>(model);

            if (model.Id == 0)
            {
                // 在调用Service类实现数据库添加
                userService.InsertUser(userEntity);
            }
            else
            {
                // 通过Service类实现编辑提交
                userService.UpdateUser(userEntity);
            }
            // 添加或编辑成功
            if (userEntity.Id > 0)
            {
                // 刷新列表
                return RedirectToAction("Index");
            }

            // 添加或编辑不成功，返回原视图，并保留原数据
            return View(model);
        }


    }
}