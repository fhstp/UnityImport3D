using System;
using System.Threading.Tasks;
using Dev.ComradeVanti;

namespace At.Ac.FhStp.Import3D
{

    internal static class TaskManagement
    {

        internal static readonly Task<Nothing> noResult =
            Task.FromResult(Nothing.atAll);

        internal static async Task<Nothing> InParallel(params Task<Nothing>[] tasks)
        {
            await Task.WhenAll(tasks);
            return Nothing.atAll;
        }

        internal static async Task<T> CalcAsync<T>(Func<T> func)
        {
            await Task.Yield();
            return func();
        }

        internal static async Task<Nothing> DoAsync(Action action)
        {
            await Task.Yield();
            action();
            return Nothing.atAll;
        }
        
    }

}