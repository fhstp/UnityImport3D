using System.Threading.Tasks;
using UnityEngine;
using static At.Ac.FhStp.Import3D.TaskManagement;

namespace At.Ac.FhStp.Import3D.Common
{
    internal static class Instantiation
    {
        internal static Task<GameObject> MakeGameObject(string name) =>
            CalcAsync(() => new GameObject(name));
    }
}