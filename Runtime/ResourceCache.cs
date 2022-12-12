using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace At.Ac.FhStp.Import3D
{

    internal abstract class ResourceCache<TKey, T>
    {

        private readonly Dictionary<TKey, Task<T>> importTasks =
            new Dictionary<TKey, Task<T>>();
        private readonly Func<TKey, Task<T>> startImport;


        protected ResourceCache(Func<TKey, Task<T>> startImport) =>
            this.startImport = startImport;


        internal Task<T> Resolve(TKey key)
        {
            if (!importTasks.ContainsKey(key))
                importTasks[key] = startImport(key);
            return importTasks[key];
        }

    }

    internal class MeshCache : ResourceCache<int, Mesh>
    {

        public MeshCache(Func<int, Task<Mesh>> startImport)
            : base(startImport) { }

    }

}