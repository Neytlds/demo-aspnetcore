using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TimeGeekBang.WebApp.Services.IoCService;

namespace TimeGeekBang.WebApp.Controllers
{
    /// <summary>
    /// 用于演示 .Net Core 默认依赖注入
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class IoCDefaultController : Controller
    {
        private readonly IOrderService _iOrderService;
        private readonly IGenericService<IOrderService> _iGenericService;

        /// <summary>
        /// 构造方法，用于演示泛型依赖注入
        /// </summary>
        /// <param name="_iOrderService"></param>
        /// <param name="_iGenericService"></param>
        public IoCDefaultController(IOrderService iOrderService, IGenericService<IOrderService> iGenericService) // 通过构造函数获取依赖注入的实例
        {
            _iOrderService = iOrderService;
            _iGenericService = iGenericService;
        }

        /// <summary>
        /// 演示三种不同生命周期的依赖注入
        /// </summary>
        [HttpGet]
        [Route("Index")]
        public int Index([FromServices] IMySingletonService _iMySingletonService1, // [FromServices]：用于从容器中获取对象
                                    [FromServices] IMySingletonService _iMySingletonService2,
                                    [FromServices] IMyTransientService _iMyTransientService1,
                                    [FromServices] IMyTransientService _iMyTransientService2,
                                    [FromServices] IMyScopedService _iMyScopedService1,
                                    [FromServices] IMyScopedService _iMyScopedService2)
        {
            Console.WriteLine($"单例模式的 HashCode 是相同的：");
            Console.WriteLine($"_iMySingletonService1: {_iMySingletonService1.GetHashCode()}");
            Console.WriteLine($"_iMySingletonService2: {_iMySingletonService2.GetHashCode()}");
            Console.WriteLine($"");

            Console.WriteLine($"瞬时模式的 HashCode 是不同的：");
            Console.WriteLine($"_iMyTransientService1: {_iMyTransientService1.GetHashCode()}");
            Console.WriteLine($"_iMyTransientService2: {_iMyTransientService2.GetHashCode()}");
            Console.WriteLine($"");

            Console.WriteLine($"作用域模式的 HashCode 在单次访问时相同的，再次访问是不同的。即每次请求生成新的对象：");
            Console.WriteLine($"_iMyScopedService1: {_iMyScopedService1.GetHashCode()}");
            Console.WriteLine($"_iMyScopedService2: {_iMyScopedService2.GetHashCode()}");
            Console.WriteLine($"");

            return 1;
        }

        /// <summary>
        /// 获取注册过的所有 IOrderService 对象，并将对象名称和 HashCode 打印出来
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetServiceList")]
        public int GetServiceList([FromServices] IEnumerable<IOrderService> services)
        {
            foreach (var item in services)
            {
                Console.WriteLine($"获取服务实例：{item}，HashCode：{item.GetHashCode()}");
            }
            return 1;
        }

        /// <summary>
        /// 用于演示 Transient 注册模式的对象释放（注意：开始测试前需修改 Startup 中“#region 作用域与对象释放行为”代码块的代码）
        /// </summary>
        /// <param name="orderService"></param>
        /// <param name="orderService2"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DisposableTransient")]
        public int DisposableTransient([FromServices] IDisposableTransientService _iDisposableTransientService, [FromServices] IDisposableTransientService _iDisposableTransientService2)
        {
            Console.WriteLine("Action 执行结束");
            return 1;
        }

        /// <summary>
        /// 用于演示 Scoped 注册模式的对象释放（注意：开始测试前需修改 Startup 中“#region 作用域与对象释放行为”代码块的代码）
        /// </summary>
        /// <param name="orderService"></param>
        /// <param name="orderService2"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DisposableScoped")]
        public int DisposableScoped([FromServices] IDisposableScopedService _iDisposableScopedService, [FromServices] IDisposableScopedService _iDisposableScopedService2)
        {
            Console.WriteLine("------1------");
            using (IServiceScope scop = HttpContext.RequestServices.CreateScope())
            {
                var service = scop.ServiceProvider.GetService<IDisposableScopedService>();
                var service2 = scop.ServiceProvider.GetService<IDisposableScopedService>();
            }
            Console.WriteLine("------2------");
            Console.WriteLine("Action 执行结束");
            return 1;

            /*
             * 由于每个作用域只能获得一个对象，所以 using 代码块中的 service 和 service2 只能得到相同的对象
             * 
             * 执行结果如下：
             * 
             * ------1------
             * DisposableScopedService.Dispose(): 680171
             * ------2------
             * Action 执行结束
             * DisposableScopedService.Dispose(): 25584554
            */
        }

        /// <summary>
        /// 用于演示 Singleton 注册模式的对象释放
        /// </summary>
        /// <param name="orderService"></param>
        /// <param name="orderService2"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DisposableSingleton")]
        public int DisposableSingleton([FromServices] IHostApplicationLifetime _iHostApplicationLifetime, [FromServices] IDisposableSingletonService _iDisposableSingletonService, [FromServices] IDisposableSingletonService _iDisposableSingletonService2)
        {
            Console.WriteLine("------1------");
            using (IServiceScope scop = HttpContext.RequestServices.CreateScope())
            {
                var service = scop.ServiceProvider.GetService<IDisposableSingletonService>();
                var service2 = scop.ServiceProvider.GetService<IDisposableSingletonService>();
            }
            Console.WriteLine("------2------");
            Console.WriteLine("Action 执行结束");
            _iHostApplicationLifetime.StopApplication(); // 关闭应用程序
            return 1;

            /*
             * 从结果可以看出，单例模式是不会释放对象的，只有在应用程序关闭时才会释放
             * 
             * 执行结果如下：
             * 
             * ------1------
             * ------2------
             * Action 执行结束
             * info: Microsoft.Hosting.Lifetime[0]
             *      Application is shutting down...
             * DisposableSingletonService.Dispose(): 30607723
             * 
             * C:\Users\Jian He\Documents\GitHub\Neytlds\demo-aspnetcore\TimeGeekBang\TimeGeekBang.WebApp\bin\Debug\netcoreapp3.1\TimeGeekBang.WebApp.exe (进程 4724)已退出，代码为 0。
             * 要在调试停止时自动关闭控制台，请启用“工具”->“选项”->“调试”->“调试停止时自动关闭控制台”。
             * 按任意键关闭此窗口. . .
            */
        }
    }
}