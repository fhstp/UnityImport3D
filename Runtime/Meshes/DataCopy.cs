using System.Threading.Tasks;
using Dev.ComradeVanti;
using static At.Ac.FhStp.Import3D.TaskManagement;

namespace At.Ac.FhStp.Import3D.Meshes
{

    internal static class DataCopy
    {

        internal static Task<Nothing> CopyVertices(MeshModel model, UnityEngine.Mesh mesh) =>
            DoAsync(async () =>
            {
                var vertices = await InBackground(() => model.Vertices.Value);
                mesh.vertices = vertices;
            });

        internal static Task<Nothing> CopyTriangles(MeshModel model, UnityEngine.Mesh mesh) =>
            DoAsync(async () =>
            {
                var triangles = await InBackground(() => model.Triangles.Value);
                mesh.triangles = triangles;
            });

        internal static Task<Nothing> CopyNormals(MeshModel model, UnityEngine.Mesh mesh) =>
            DoAsync(async () =>
            {
                var normals = await InBackground(() => model.Normals.Value);
                mesh.normals = normals;
            });

    }

}