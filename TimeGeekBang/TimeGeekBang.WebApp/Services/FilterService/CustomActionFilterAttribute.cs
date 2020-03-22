using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeGeekBang.WebApp.Services.FilterService
{
    /// <summary>
    /// 自定义 ActionFilter 过滤器
    /// </summary>
    public class CustomActionFilterAttribute : ActionFilterAttribute, IActionFilter, IAsyncActionFilter
    {
        private ILogger _logger = null;

        public CustomActionFilterAttribute()
        {
        }

        public CustomActionFilterAttribute(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CustomActionFilterAttribute>();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("this is - public class CustomActionFilterAttribute : ActionFilterAttribute, IActionFilter, _OnActionExecuting");
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("this is - public class CustomActionFilterAttribute : ActionFilterAttribute, IActionFilter, _OnActionExecuted");
            base.OnActionExecuted(context);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine("this is - public class CustomActionFilterAttribute : ActionFilterAttribute, IActionFilter, _OnResultExecuting");
            base.OnResultExecuting(context);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("this is - public class CustomActionFilterAttribute : ActionFilterAttribute, IActionFilter, _OnResultExecuted");
            base.OnResultExecuted(context);
        }
    }
}
