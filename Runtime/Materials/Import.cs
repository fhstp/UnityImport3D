using System.Threading.Tasks;
using Dev.ComradeVanti;
using UnityEngine;
using static At.Ac.FhStp.Import3D.Common.DataCopy;
using static At.Ac.FhStp.Import3D.Materials.DataCopy;
using static At.Ac.FhStp.Import3D.Materials.Instantiation;
using static At.Ac.FhStp.Import3D.Materials.ModelConversion;
using static At.Ac.FhStp.Import3D.TaskManagement;

namespace At.Ac.FhStp.Import3D.Materials
{
    internal static class Import
    {
        private static readonly int color = Shader.PropertyToID("_Color");
        private static readonly int specColor = Shader.PropertyToID("_SpecColor");

        internal static async Task<Material> ImportMeshFromModel(MaterialModel model)
        {
            var material = await MakeMaterial();

            await InParallel(
                CopyName(model, material),
                CopyColor(model.Color, color, material),
                CopyColor(model.SpecularColor, specColor, material),
                model.EmissiveColor.Match(
                    it => SetEmissiveColor(it, material),
                    () => Task.FromResult(Nothing.atAll)),
                model.IsTransparent ? SetTransparent(material) : SetOpaque(material),
                CopySmoothness(model.Smoothness, material));

            return material;
        }

        internal static async Task<Material> ImportMaterial(Assimp.Material assimpMaterial)
        {
            var model = await InBackground(() => ConvertToModel(assimpMaterial));
            return await ImportMeshFromModel(model);
        }
    }
}