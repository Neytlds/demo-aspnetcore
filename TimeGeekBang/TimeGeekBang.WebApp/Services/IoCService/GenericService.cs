using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeGeekBang.WebApp.Services.IoCService
{
    /// <summary>
    /// 用于演示泛型依赖注入
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericService<T>
    {
    }
    
    public class GenericService<T> : IGenericService<T>
    {
        public T Data { get; private set; }

        public GenericService(T data)
        {
            this.Data = data;
        }
    }
}
