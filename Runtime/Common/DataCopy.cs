using System.Threading.Tasks;
using Dev.ComradeVanti;
using UnityObject = UnityEngine.Object;
using static At.Ac.FhStp.Import3D.TaskManagement;

namespace At.Ac.FhStp.Import3D.Common
{

    internal static class DataCopy
    {

        internal static Task<Nothing> CopyName(
            INamedModel model, UnityObject obj) =>
            DoAsync(() => obj.name = model.Name);

    }

}