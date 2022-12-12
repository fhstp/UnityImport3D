using System.Linq;
using System.Threading.Tasks;
using Dev.ComradeVanti;
using UnityEngine;
using UnityTexture = UnityEngine.Texture2D;
using static At.Ac.FhStp.Import3D.TaskManagement;

namespace At.Ac.FhStp.Import3D.DataCopy
{

    internal static class Texture2D
    {

        internal static Task<Nothing> CopyTextureBytes(
            CompressedTextureModel model, UnityTexture texture) =>
            DoAsync(() => texture.LoadImage(model.Bytes.ToArray()));

        internal static Task<Nothing> CopyTexturePixels(
            NonCompressedTextureModel model, UnityTexture texture) =>
            DoAsync(() =>
            {
                texture.SetPixels(model.Pixels.ToArray());
                texture.Apply();
            });

    }

}