using System;
using System.Threading.Tasks;

namespace Common.Singletons
{
    public interface ILocker
    {
        Task ExecuteAsync(string uniqId, Func<Task> operation);
    }
}
