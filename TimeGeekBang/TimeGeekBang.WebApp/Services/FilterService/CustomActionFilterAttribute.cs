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

        /// <summary>
        /// 在操作执行之前调用，在模型绑定完成之后调用
        /// 原文：Called before the action executes, after model binding is complete.
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("this is - public class CustomActionFilterAttribute : ActionFilterAttribute, IActionFilter, _OnActionExecuting");
            base.OnActionExecuting(context);
        }

        /// <summary>
        /// 在操作执行之后，在操作结果之前调用
        /// 原文：Called after the action executes, before the action result.
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("this is - public class CustomActionFilterAttribute : ActionFilterAttribute, IActionFilter, _OnActionExecuted");
            base.OnActionExecuted(context);
        }

        /// <summary>
        /// 在操作结果执行之前调用
        /// 原文：Called before the action result executes.
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine("this is - public class CustomActionFilterAttribute : ActionFilterAttribute, IActionFilter, _OnResultExecuting");
            base.OnResultExecuting(context);
        }

        /// <summary>
        /// 在操作结果执行后调用
        /// 原文：Called after the action result executes.
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("this is - public class CustomActionFilterAttribute : ActionFilterAttribute, IActionFilter, _OnResultExecuted");
            base.OnResultExecuted(context);
        }
    }
}
