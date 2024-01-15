using System;
using System.Collections.Generic;
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

        internal static async Task<(T1, T2)> InParallel<T1, T2>(Task<T1> t1, Task<T2> t2)
        {
            var v1 = await t1;
            var v2 = await t2;
            return (v1, v2);
        }

        internal static async Task<(T1, T2, T3)> InParallel<T1, T2, T3>(Task<T1> t1, Task<T2> t2, Task<T3> t3)
        {
            var v1 = await t1;
            var v2 = await t2;
            var v3 = await t3;
            return (v1, v2, v3);
        }

        internal static async Task<IEnumerable<T>> InParallel<T>(IEnumerable<Task<T>> tasks)
        {
            var values = await Task.WhenAll(tasks);
            return values;
        }

        internal static async Task<T> CalcAsync<T>(Func<T> func)
        {
            await Task.Yield();
            return func();
        }

        internal static async Task<T> CalcAsync<T>(Func<Task<T>> func)
        {
            await Task.Yield();
            return await func();
        }

        internal static async Task<Nothing> DoAsync(Action action)
        {
            await Task.Yield();
            action();
            return Nothing.atAll;
        }

        internal static async Task<Nothing> DoAsync(Func<Task> action)
        {
            await Task.Yield();
            await action();
            return Nothing.atAll;
        }


        internal static Task<T> InBackground<T>(Func<T> func) =>
            Task.Run(func);
    }
}