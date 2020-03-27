using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// 用于演示依赖注入对象释放
/// </summary>
namespace TimeGeekBang.WebApp.Services.IoCService
{
    public interface IDisposableTransientService
    {
    }

    public class DisposableTransientService: IDisposableTransientService, IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine($"DisposableTransientService.Dispose(): {GetHashCode()}");
        }
    }


    public interface IDisposableScopedService
    {
    }

    public class DisposableScopedService : IDisposableScopedService, IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine($"DisposableScopedService.Dispose(): {GetHashCode()}");
        }
    }


    public interface IDisposableSingletonService
    {
    }

    public class DisposableSingletonService : IDisposableSingletonService, IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine($"DisposableSingletonService.Dispose(): {GetHashCode()}");
        }
    }

}
