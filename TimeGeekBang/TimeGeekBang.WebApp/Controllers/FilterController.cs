using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeGeekBang.WebApp.Services.FilterService;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeGeekBang.WebApp.Controllers
{
    /// <summary>
    /// 一些过滤器的例子
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class FilterController : Controller
    {
        /// <summary>
        /// 这里演示了如何使用 ExceptionFilter 捕捉异常
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ExceptionFilter")]
        [CustomExceptionFilter]
        public IActionResult ExceptionFilter()
        {
            int i = 10;
            int j = 0;
            int x = i / j; // 这里将产生除数为 0 的异常（注意：调试的时候这里会先抛出异常，点击 F5 继续执行后才能进入过滤器执行异常的后续操作）
            return new JsonResult(x);
        }

        /// <summary>
        /// 这个 Action 演示了如何使用 ActionFilter 记录日志
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ActionFilter")]
        [CustomActionFilter]
        public IActionResult ActionFilter(int id, string name)
        {
            // 1、记录日志：记录Id，name

            // To do

            // 2、在记录一下执行结果

            return new JsonResult(new { Id = id, Name = name });
        }
    }
}
