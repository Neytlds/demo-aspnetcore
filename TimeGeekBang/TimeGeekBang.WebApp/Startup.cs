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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
