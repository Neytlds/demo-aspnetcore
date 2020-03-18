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
        // ����ע��
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(); // ע�� Controller ����ע����Ϊ�� WebAPI ��Ŀ������ֻע�� Controller �Ϳ����ˣ�MVC ��ĿӦ����ע�� services.AddMVC();��
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // ����ָ�� asp.net core web �����������Ӧÿһ�� http ����ģ���������������Ĺܵ�������ͨ������м���ķ�ʽ������Щ�ܵ�
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // �жϵ�ǰ�Ƿ�Ϊ��������������ǣ�����ϵͳ�����쳣ʱ���ع�����ʱ���쳣ҳ��
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting(); // ·���м��

            app.UseAuthorization(); // Ϊ web �����ṩ�û���Ȩ��������������Ҫ������� ConfigureServices �����н������ò���ʹ��

            // ʹ�ö˵㡣���ڴ���·�ɣ�������ΰ� http ������䵽�ض��� controller �� action ����
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
