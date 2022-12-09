using System;
using Dev.ComradeVanti;
using Object = UnityEngine.Object;

namespace At.Ac.FhStp.Import3D
{

    internal static partial class Instantiate
    {

        private static Func<Nothing> SetName(Object o, string name) =>
            () =>
            {
                o.name = name;
                return Nothing.atAll;
            };

    }

}