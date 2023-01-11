using System.Threading.Tasks;
using UnityEngine;
using static At.Ac.FhStp.Import3D.TaskManagement;

namespace At.Ac.FhStp.Import3D.Meshes
{
    internal static class Instantiation
    {
        internal static Task<Mesh> MakeEmptyMesh() =>
            CalcAsync(() => new Mesh());
    }
}