using System.Threading.Tasks;
using UnityEngine;
using static At.Ac.FhStp.Import3D.TaskManagement;

namespace At.Ac.FhStp.Import3D.Common
{
    internal static class Instantiation
    {
        internal static Task<GameObject> MakeGameObject(string name, bool active) =>
            CalcAsync(() =>
            {
                var gameObject = new GameObject(name);
                gameObject.SetActive(active);
                return gameObject;
            });
    }
}