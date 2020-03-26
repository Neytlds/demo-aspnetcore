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
        // ����ע��
        public void ConfigureServices(IServiceCollection services)
        {
            #region ע�᲻ͬ�������ڵķ���

            services.AddSingleton<IMySingletonService, MySingletonService>(); // ����ģʽע�����
            services.AddScoped<IMyScopedService, MyScopedService>(); // ������ģʽע�����
            services.AddTransient<IMyTransientService, MyTransientService>(); // ˲ʱģʽע�����

            #endregion


            #region ��ͬ��ע��д��

            services.AddSingleton<IOrderService>(new OrderService()); // ֱ��ʹ��ʵ��ע��

            // ʹ�ù�����ʽע��
            services.AddScoped<IOrderService>(ServiceProvider => 
            {
                return new OrderServiceEx();
            });

            // ʹ�ù�����ʽע��
            services.AddScoped<IOrderService>(ServiceProvider =>
            {
                //ServiceProvider.GetService<>(); // ͨ��ʹ�� IServiceProvider �����ʵ�ֻ�ȡ�Ը�����Ȼ�������װ�õ�ʵ��������ʵ���������������е���һ���࣬����ʹ������һ�����װԭ�е�ʵ����
                return new OrderServiceEx2();
            });

            #endregion


            #region ����ע�᣺��������Ѿ�ע����ˣ��Ͳ���ע����

            services.TryAddSingleton<IOrderService, OrderServiceEx>(); // ����ӿ������Ѿ�ע����ˣ��Ͳ���ע����
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IOrderService, OrderService>()); // �ӿ�������ͬ��ʵ���಻ͬ�Ϳ���ע�ᣬ���ʵ������ͬ�Ͳ�ע��

            #endregion


            #region �Ƴ����滻

            services.Replace(ServiceDescriptor.Singleton<IOrderService, OrderServiceEx>()); // �� IOrderService ��ʵ�����滻Ϊ OrderServiceEx
            services.RemoveAll<IOrderService>(); // �Ƴ����� IOrderService ��ע��

            #endregion


            #region ע�᷺��ģ��

            // ע��һ�鷺������ʱ�����ڲ�֪�����͵������ʲô�����Կ���ʹ������ע�����ṩ�ķ���ģ��ע�뷽ʽ������ζ��ͨ�����д������ע�����д˷��͵�ʵ����
            services.AddSingleton(typeof(IGenericService<>),typeof(GenericService<>));

            #endregion


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
