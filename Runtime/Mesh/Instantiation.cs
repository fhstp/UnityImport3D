using System.Threading.Tasks;
using static At.Ac.FhStp.Import3D.TaskManagement;

namespace At.Ac.FhStp.Import3D.Mesh
{

    internal static class Instantiation
    {

        internal static Task<UnityEngine.Mesh> MakeEmptyMesh() =>
            CalcAsync(() => new UnityEngine.Mesh());

    }

}