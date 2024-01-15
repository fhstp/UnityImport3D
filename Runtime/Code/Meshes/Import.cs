#nullable enable

using System.Threading.Tasks;
using UnityEngine;
using AssimpMesh = Assimp.Mesh;
using static At.Ac.FhStp.Import3D.Meshes.Instantiation;
using static At.Ac.FhStp.Import3D.Meshes.ModelConversion;
using static At.Ac.FhStp.Import3D.Common.DataCopy;
using static At.Ac.FhStp.Import3D.Meshes.DataCopy;
using static At.Ac.FhStp.Import3D.TaskManagement;

namespace At.Ac.FhStp.Import3D.Meshes
{
    internal static class Import
    {
        internal static async Task<Mesh> ImportMeshFromModel(MeshModel model)
        {
            var mesh = await MakeEmptyMesh();

            await InParallel(
                CopyName(model, mesh),
                CopyVertices(model, mesh));
            await CopyTriangles(model, mesh);
            await CopyNormals(model, mesh);

            return mesh;
        }

        internal static async Task<Mesh> ImportMesh(AssimpMesh assimpMesh, MeshImportConfig config)
        {
            var model = await InBackground(
                () => ConvertToModel(assimpMesh, config.ScalingFactor));
            return await ImportMeshFromModel(model);
        }

    }
}