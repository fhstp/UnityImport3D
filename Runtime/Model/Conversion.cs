using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace At.Ac.FhStp.Import3D
{

    internal class Conversion<T>
    {

        private readonly Lazy<Task<T>> makeModel;


        private Conversion(Func<Task<T>> makeModel) =>
            this.makeModel = new Lazy<Task<T>>(makeModel);


        internal static Conversion<T> Const(T model) =>
            Async(() => Task.FromResult(model));

        internal static Conversion<T> Sync(Func<T> makeModel) =>
            Async(() => Task.FromResult(makeModel()));

        internal static Conversion<T> Async(Func<Task<T>> makeModel) =>
            new Conversion<T>(makeModel);

        internal Task<T> Start() =>
            makeModel.Value;

        public TaskAwaiter<T> GetAwaiter() =>
            Start().GetAwaiter();

    }

    internal static class Ext
    {

        public static Conversion<IEnumerable<T>> InParallel<T>(
            this IEnumerable<Conversion<T>> conversions) =>
            Conversion<IEnumerable<T>>.Async(
                async () =>
                {
                    var tasks = conversions.Select(it => it.Start());
                    var results = await Task.WhenAll(tasks);
                    return results;
                });

    }

}