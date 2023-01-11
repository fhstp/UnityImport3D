using System.Collections.Generic;
using System.Linq;
using Assimp;
using AssimpMesh = Assimp.Mesh;

namespace At.Ac.FhStp.Import3D
{
    internal static class AssimpMeshBuilding
    {
        internal static AssimpMesh MakeEmptyMesh(string name) => new(name);

        internal static AssimpMesh MakePointMesh(
            string name, IEnumerable<Vector3D> points)
        {
            var mesh = new AssimpMesh(name, PrimitiveType.Point);
            mesh.Vertices.AddRange(points);
            return mesh;
        }

        internal static AssimpMesh MakeTriangleMesh(
            string name, IEnumerable<Vector3D> vertices,
            IEnumerable<(int, int, int)> triangles)
        {
            var mesh = new AssimpMesh(name, PrimitiveType.Triangle);
            var faces = triangles.Select(tri => new Face(new[]
            {
                tri.Item1, tri.Item2, tri.Item3
            }));

            mesh.Vertices.AddRange(vertices);
            mesh.Faces.AddRange(faces);
            return mesh;
        }

        internal static AssimpMesh MakeQuad(string name) =>
            MakeTriangleMesh(name, new[]
            {
                new Vector3D(0, 0, 0),
                new Vector3D(0, 1, 0),
                new Vector3D(1, 1, 0),
                new Vector3D(1, 0, 0)
            }, new[]
            {
                (0, 1, 2),
                (2, 3, 0)
            });

        internal static AssimpMesh MakeQuadWithNormals(
            string name, Vector3D normal1, Vector3D normal2, Vector3D normal3,
            Vector3D normal4)
        {
            var mesh = MakeQuad(name);
            mesh.Normals.AddRange(new[] {normal1, normal2, normal3, normal4});
            return mesh;
        }
    }
}