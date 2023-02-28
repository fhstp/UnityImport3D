using System.Threading.Tasks;
using Dev.ComradeVanti;
using UnityEngine;
using static At.Ac.FhStp.Import3D.TaskManagement;

namespace At.Ac.FhStp.Import3D.Materials
{
    internal static class DataCopy
    {

        internal static Task<Nothing> CopyColor(
            Color color, string name, Material material) =>
            DoAsync(() => material.SetColor(name, color));

    }
}