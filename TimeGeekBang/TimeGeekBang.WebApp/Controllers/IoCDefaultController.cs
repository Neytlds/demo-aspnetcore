using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        /// <summary>
        /// 
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
    }
}