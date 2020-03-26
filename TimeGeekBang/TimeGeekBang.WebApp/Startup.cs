using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TimeGeekBang.WebApp.Services.IoCService;

namespace TimeGeekBang.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // 依赖注入
        public void ConfigureServices(IServiceCollection services)
        {
            #region 注册不同生命周期的服务

            services.AddSingleton<IMySingletonService, MySingletonService>(); // 单例模式注册服务
            services.AddScoped<IMyScopedService, MyScopedService>(); // 作用域模式注册服务
            services.AddTransient<IMyTransientService, MyTransientService>(); // 瞬时模式注册服务

            #endregion


            #region 不同的注册写法

            services.AddSingleton<IOrderService>(new OrderService()); // 直接使用实例注册

            // 使用工厂方式注册
            services.AddScoped<IOrderService>(ServiceProvider => 
            {
                return new OrderServiceEx();
            });

            // 使用工厂方式注册
            services.AddScoped<IOrderService>(ServiceProvider =>
            {
                //ServiceProvider.GetService<>(); // 通过使用 IServiceProvider 的入参实现获取对个对象，然后进行组装得到实例。比如实现类依赖了容器中的另一个类，或者使用另外一个类包装原有的实现类
                return new OrderServiceEx2();
            });

            #endregion


            #region 尝试注册：如果服务已经注册过了，就不再注册了

            services.TryAddSingleton<IOrderService, OrderServiceEx>(); // 如果接口类型已经注册过了，就不再注册了
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IOrderService, OrderService>()); // 接口类型相同，实现类不同就可以注册，如果实现类相同就不注册

            #endregion


            #region 移除和替换

            services.Replace(ServiceDescriptor.Singleton<IOrderService, OrderServiceEx>()); // 将 IOrderService 的实现类替换为 OrderServiceEx
            services.RemoveAll<IOrderService>(); // 移除所有 IOrderService 的注册

            #endregion


            #region 注册泛型模板

            // 注册一组泛型类型时，由于不知道泛型的入参是什么，所以可以使用依赖注入框架提供的泛型模板注入方式。这意味着通过这行代码可以注册所有此泛型的实现类
            services.AddSingleton(typeof(IGenericService<>),typeof(GenericService<>));

            #endregion


            services.AddControllers(); // 注册 Controller 服务（注：因为是 WebAPI 项目，所以只注册 Controller 就可以了，MVC 项目应该是注册 services.AddMVC();）
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // 用于指定 asp.net core web 程序是如何响应每一个 http 请求的，这里配置了请求的管道，可以通过添加中间件的方式配置这些管道
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 判断当前是否为开发环境，如果是，则在系统出现异常时返回供开发时的异常页面
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting(); // 路由中间件

            app.UseAuthorization(); // 为 web 程序提供用户授权的能力，但是需要在上面的 ConfigureServices 方法中进行配置才能使用

            // 使用端点。用于处理路由，用于如何把 http 请求分配到特定的 controller 或 action 上面
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
