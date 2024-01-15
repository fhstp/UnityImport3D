using System.Threading.Tasks;
using UnityEngine;
using UnityObject = UnityEngine.Object;
using static At.Ac.FhStp.Import3D.TaskManagement;

namespace At.Ac.FhStp.Import3D.Textures
{
    internal static class Instantiation
    {
        internal static Task<Texture2D> MakeTexture2DWithSize(int width, int height) =>
            CalcAsync(() => new Texture2D(width, height));

        internal static Task<Texture2D> MakeEmptyTexture2D() =>
            MakeTexture2DWithSize(0, 0);
    }
}