using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeGeekBang.WebApp.Services.FilterService
{
    /// <summary>
    /// 自定义异常（ExceptionFilter）过滤器
    /// </summary>
    public class CustomExceptionFilterAttribute : Attribute, IExceptionFilter//, IAsyncExceptionFilter
    {
        private ILogger _logger = null;

        /// <summary>
        /// 为方便标记特定此处无参构造函数
        /// </summary>
        public CustomExceptionFilterAttribute()
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="loggerFactory">注入 Logger</param>
        public CustomExceptionFilterAttribute(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CustomExceptionFilterAttribute>();
        }

        /// <summary>
        /// 异常发生时的处理程序（同步）
        /// </summary>
        /// <param name="context">异常信息</param>
        public void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled == false) // 在处理异常前，一定要先判断一下异常是否被处理过
            {
                context.Result = new JsonResult("this is a exception."); // 通过 Result 短路器设置返回消息
                context.ExceptionHandled = true; // 异常处理后，将异常状态设置为：已处理
            }
        }

        ///// <summary>
        ///// 异常发生时的处理程序（异步）
        ///// </summary>
        ///// <param name="context">异常信息</param>
        ///// <returns></returns>
        //public Task OnExceptionAsync(ExceptionContext context)
        //{
        //    return Task.Run(() => { Console.WriteLine("this is CustomExceptionFilterAttribute_OnExceptionAsync."); });
        //}
    }
}
