using System.Threading.Tasks;
using AssimpMesh = Assimp.Mesh;
using static At.Ac.FhStp.Import3D.Mesh.Instantiation;
using static At.Ac.FhStp.Import3D.Mesh.ModelConversion;
using static At.Ac.FhStp.Import3D.Common.DataCopy;
using static At.Ac.FhStp.Import3D.Mesh.DataCopy;
using static At.Ac.FhStp.Import3D.TaskManagement;

namespace At.Ac.FhStp.Import3D.Mesh
{

    internal static class Import
    {

        internal static async Task<UnityEngine.Mesh> ImportMesh(AssimpMesh assimpMesh)
        {
            var model = await InBackground(() => ConvertToModel(assimpMesh));
            var mesh = await MakeEmptyMesh();

            await InParallel(
                CopyName(model, mesh),
                CopyVertices(model, mesh));
            await CopyTriangles(model, mesh);
            await CopyNormals(model, mesh);

            return mesh;
        }

    }

}