using AutoMapper;
using Core.Repository;
using Core.UnitofWork;
using Entities;
using Ninject;
using Ninject.Web.Common.WebHost;
using Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication1.Models;

namespace WebApplication1
{
    /*
        在启动类调用Ninject绑定所有对象的依赖注入 
     */

    /* 原有默认启动类:
        public class MvcApplication : System.Web.HttpApplication
        {
            protected void Application_Start()
            {
                AreaRegistration.RegisterAllAreas();
                RouteConfig.RegisterRoutes(RouteTable.Routes);
            }
        }
    */

    public class MvcApplication : NinjectHttpApplication
    {
        // 依赖注入的配置
        protected override IKernel CreateKernel()
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            // 绑定关系如下:
            kernel.Bind<DbContext>().To<IocDbContext>();
            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            kernel.Bind<IUnitofWork>().To<UnitofWork>();
            kernel.Bind<IUserService>().To<UserService>();

            return kernel;
        }
       
        // MVC的配置
        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

        }
    }
}
