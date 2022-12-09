using System.Threading.Tasks;
using Dev.ComradeVanti;
using UnityEngine;
using static At.Ac.FhStp.Import3D.TaskManagement;

namespace At.Ac.FhStp.Import3D
{

    internal static partial class Instantiate
    {

        private static Task<Nothing> SetName(Object o, string name)
        {
            o.name = name;
            return noResult;
        }

    }

}