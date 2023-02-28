using System.Threading.Tasks;
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
                CopyColor(model.Color, "_Color", material));
            
            return material;
        }

        internal static async Task<Material> ImportMaterial(Assimp.Material assimpMaterial)
        {
            var model = await InBackground(() => ConvertToModel(assimpMaterial));
            return await ImportMeshFromModel(model);
        }

    }
}