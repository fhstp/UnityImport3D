using System;
using System.Linq;
using System.Threading.Tasks;
using Dev.ComradeVanti;

namespace At.Ac.FhStp.Import3D
{

    internal interface ITaskRunner
    {

        Task<T> Run<T>(Func<Task<T>> func);

    }

    internal static class TaskRunner
    {

        internal static ITaskRunner ForCurrentThread()
        {
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            var factory = new TaskFactory(scheduler);
            return new ThreadTaskRunner(factory);
        }


        private class ThreadTaskRunner : ITaskRunner
        {

            private readonly TaskFactory taskFactory;


            public ThreadTaskRunner(TaskFactory taskFactory) =>
                this.taskFactory = taskFactory;


            public async Task<T> Run<T>(Func<Task<T>> func)
            {
                var task = await taskFactory.StartNew(func);
                return await task;
            }

        }

    }

    // ReSharper disable once InconsistentNaming
    internal static class ITaskRunnerExt
    {

        internal static Task<T> Run<T>(this ITaskRunner runner, Func<T> func) =>
            runner.Run(() => Task.FromResult(func()));

        internal static Task<Nothing> Run(this ITaskRunner runner, Action action) =>
            runner.Run(() =>
            {
                action();
                return Nothing.atAll;
            });

        internal static Task<Nothing> RunInParallel(
            this ITaskRunner runner, params Func<Nothing>[] funcs) =>
            runner.Run(async () =>
            {
                await Task.WhenAll(funcs.Select(runner.Run));
                return Nothing.atAll;
            });

    }

}