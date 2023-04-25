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
        internal static async Task<Material> ImportMeshFromModel(MaterialModel model)
        {
            var material = await MakeMaterial();

            await InParallel(
                CopyName(model, material),
                CopyColor(model.Color, material),
                CopySpecColor(model.SpecularColor, material),
                model.EmissiveColor.Match(
                    onSome: it => CopyEmissiveColor(it, material),
                    onNone: () => Task.FromResult(Nothing.atAll)),
                CopyTransparency(material, model.IsTransparent),
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