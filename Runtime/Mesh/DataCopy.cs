using System.Threading.Tasks;
using Dev.ComradeVanti;
using static At.Ac.FhStp.Import3D.TaskManagement;

namespace At.Ac.FhStp.Import3D.Mesh
{

    internal static class DataCopy
    {

        internal static Task<Nothing> CopyVertices(MeshModel model, UnityEngine.Mesh mesh) =>
            DoAsync(() =>
            {
                var vertices = model.Vertices.Value;
                mesh.vertices = vertices;
            });

        internal static Task<Nothing> CopyTriangles(MeshModel model, UnityEngine.Mesh mesh) =>
            DoAsync(() =>
            {
                var triangles = model.Triangles.Value;
                mesh.triangles = triangles;
            });

        internal static Task<Nothing> CopyNormals(MeshModel model, UnityEngine.Mesh mesh) =>
            DoAsync(() =>
            {
                var normals = model.Normals.Value;
                mesh.normals = normals;
            });
        
    }

}