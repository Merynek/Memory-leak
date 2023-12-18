using Common.Enums;
using Common.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Singletons
{
    public class Locker : ILocker
    {
        private ConcurrentDictionary<string, string> _lockMap = new ConcurrentDictionary<string, string>();

        public async Task ExecuteAsync(string uniqId, Func<Task> operation)
        {
            if (_isLocked(uniqId))
            {
                throw new BadRequestSUBException(ErrorCode.UNKNOWN, uniqId + " - not support conccurence process!");
            }
            else
            {
                try
                {
                    _lock(uniqId);
                    await operation.Invoke();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    _unlock(uniqId);
                }
            }
        }

        private void _lock(string uniqId)
        {
            _lockMap.TryAdd(uniqId, uniqId);
        }

        private void _unlock(string uniqId)
        {
            _lockMap.Remove(uniqId, out var x);
        }

        private bool _isLocked(string uniqId)
        {
            return _lockMap.ContainsKey(uniqId);
        }
    }
}
