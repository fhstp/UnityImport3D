using System.Linq;
using System.Threading.Tasks;
using Assimp;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using static At.Ac.FhStp.Import3D.AssimpMeshBuilding;
using static At.Ac.FhStp.Import3D.Mesh.Import;

namespace At.Ac.FhStp.Import3D
{

    [RequiresPlayMode]
    public class MeshImportTests
    {

        [Test]
        public async Task Mesh_Name_Is_Assimp_Name()
        {
            const string name = "Super mesh";
            var assimpMesh = MakeEmptyMesh(name);

            var mesh = await ImportMesh(assimpMesh);

            Assert.AreEqual(name, mesh.name);
        }

        [Test]
        public async Task Mesh_Vertices_Are_Converted_Assimp_Vertices()
        {
            var assimpMesh = MakePointMesh("My mesh", new[]
            {
                new Vector3D(1, 2, 3),
                new Vector3D(4.5f, -6.7f, -8)
            });

            var mesh = await ImportMesh(assimpMesh);
            var vertices = mesh.vertices;

            Assert.AreEqual(2, vertices.Length, "Length");
            Assert.AreEqual(new Vector3(1, 2, 3), vertices[0], "Vertex 1");
            Assert.AreEqual(new Vector3(4.5f, -6.7f, -8), vertices[1], "Vertex 2");
        }

        [Test]
        public async Task Mesh_Triangles_Are_Assimp_Triangles()
        {
            var assimpMesh = MakeTriangleMesh(
                "My mesh",
                new[]
                {
                    new Vector3D(1, 0, 0),
                    new Vector3D(0, 1, 0),
                    new Vector3D(0, 0, 1),
                    new Vector3D(0, 0, 0)
                }, new[]
                {
                    (0, 1, 2),
                    (1, 2, 3)
                });

            var mesh = await ImportMesh(assimpMesh);
            var triangles = mesh.triangles;

            Assert.AreEqual(6, triangles.Length, "Length");
            Assert.AreEqual(new[] { 0, 1, 2 }, triangles.Skip(0).Take(3));
            Assert.AreEqual(new[] { 1, 2, 3 }, triangles.Skip(3).Take(3));
        }


        [Test]
        public async Task Mesh_Normals_Are_Converted_Assimp_Normals()
        {
            var assimpMesh = MakeQuadWithNormals(
                "My mesh",
                new Vector3D(1, 0, 0), new Vector3D(0, 1, 0),
                new Vector3D(0, 0, 1), new Vector3D(1, 1, 0)
            );

            var mesh = await ImportMesh(assimpMesh);
            var normals = mesh.normals;

            Assert.AreEqual(4, normals.Length, "Length");
            Assert.AreEqual(new Vector3(1, 0, 0), normals[0], "Normal 1");
            Assert.AreEqual(new Vector3(0, 1, 0), normals[1], "Normal 2");
            Assert.AreEqual(new Vector3(0, 0, 1), normals[2], "Normal 3");
            Assert.AreEqual(new Vector3(1, 1, 0), normals[3], "Normal 4");
        }

    }

}