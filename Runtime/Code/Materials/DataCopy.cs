using System.Threading.Tasks;
using Dev.ComradeVanti;
using UnityEngine;
using static At.Ac.FhStp.Import3D.TaskManagement;

namespace At.Ac.FhStp.Import3D.Materials
{
    internal static class DataCopy
    {
        private static readonly int colorId = Shader.PropertyToID("_Color");
        private static readonly int specColorId = Shader.PropertyToID("_SpecColor");
        private static readonly int srcBlendId = Shader.PropertyToID("_SrcBlend");
        private static readonly int dstBlendId = Shader.PropertyToID("_DstBlend");
        private static readonly int zWriteId = Shader.PropertyToID("_ZWrite");
        private static readonly int emissionColorId = Shader.PropertyToID("_EmissionColor");
        private static readonly int glossinessId = Shader.PropertyToID("_Glossiness");

        internal static Task<Nothing> CopyColor(Color color, Material material) =>
            DoAsync(() => material.SetColor(colorId, color));
        
        internal static Task<Nothing> CopySpecColor(Color color, Material material) =>
            DoAsync(() => material.SetColor(specColorId, color));

        internal static Task<Nothing> CopyTransparency(Material material, bool isTransparent) =>
            DoAsync(() =>
            {
                material.SetOverrideTag("RenderType", isTransparent ? "Transparent" : "");

                material.SetInt(srcBlendId, (int) UnityEngine.Rendering.BlendMode.One);
                material.SetInt(dstBlendId, isTransparent
                    ? (int) UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha
                    : (int) UnityEngine.Rendering.BlendMode.Zero);

                material.SetInt(zWriteId, isTransparent ? 0 : 1);

                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                if (isTransparent)
                    material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                else
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");

                material.renderQueue = isTransparent ? 3000 : -1;
            });

        internal static Task<Nothing> CopyEmissiveColor(Color color, Material material) =>
            DoAsync(() =>
            {
                material.EnableKeyword("_EMISSION");
                material.SetColor(emissionColorId, color);
            });

        internal static Task<Nothing> CopySmoothness(float smoothness, Material material) =>
            DoAsync(() => material.SetFloat(glossinessId, smoothness));
    }
}