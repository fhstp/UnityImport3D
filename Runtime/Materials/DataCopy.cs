using System.Threading.Tasks;
using Dev.ComradeVanti;
using UnityEngine;
using static At.Ac.FhStp.Import3D.TaskManagement;

namespace At.Ac.FhStp.Import3D.Materials
{
    internal static class DataCopy
    {
        private static readonly int srcBlend = Shader.PropertyToID("_SrcBlend");
        private static readonly int dstBlend = Shader.PropertyToID("_DstBlend");
        private static readonly int zWrite = Shader.PropertyToID("_ZWrite");
        private static readonly int emissionColor = Shader.PropertyToID("_EmissionColor");

        internal static Task<Nothing> CopyColor(Color color, int propId, Material material) =>
            DoAsync(() => material.SetColor(propId, color));

        internal static Task<Nothing> CopyFloat(float f, int propId, Material material) =>
            DoAsync(() => material.SetFloat(propId, f));

        internal static Task<Nothing> SetOpaque(Material material) =>
            DoAsync(() =>
            {
                material.SetOverrideTag("RenderType", "");
                material.SetInt(srcBlend, (int) UnityEngine.Rendering.BlendMode.One);
                material.SetInt(dstBlend, (int) UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt(zWrite, 1);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = -1;
            });

        internal static Task<Nothing> SetTransparent(Material material) =>
            DoAsync(() =>
            {
                material.SetOverrideTag("RenderType", "Transparent");
                material.SetInt(srcBlend, (int) UnityEngine.Rendering.BlendMode.One);
                material.SetInt(dstBlend, (int) UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt(zWrite, 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
            });

        internal static Task<Nothing> SetEmissiveColor(Color color, Material material) =>
            DoAsync(() =>
            {
                material.EnableKeyword("_EMISSION");
                material.SetColor(emissionColor, color);
            });
    }
}