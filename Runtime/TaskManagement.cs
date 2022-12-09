using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dev.ComradeVanti;
using UnityEngine;

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

    }
}
