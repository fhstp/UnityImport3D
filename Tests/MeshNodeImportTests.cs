using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using static At.Ac.FhStp.Import3D.Nodes.Import;

namespace At.Ac.FhStp.Import3D
{
    [RequiresPlayMode]
    public class MeshNodeImportTests
    {
        private readonly Mesh mesh;
        private readonly MeshCache meshCache;


        public MeshNodeImportTests()
        {
            mesh = new Mesh
            {
                name = "My Mesh"
            };
            meshCache = new MeshCache(_ => Task.FromResult(mesh));
        }


        [Test]
        public async Task MeshNode_Name_Is_Mesh_Name()
        {
            var meshNode = await ImportMeshNode(0, meshCache);

            Assert.AreEqual(mesh.name, meshNode.name);
        }

        [Test]
        public async Task MeshNode_Has_MeshFilter_With_Mesh()
        {
            var meshNode = await ImportMeshNode(0, meshCache);

            var meshFilter = meshNode.GetComponent<MeshFilter>();
            Assert.True(meshFilter, "Has mesh-filter");

            Assert.AreEqual(mesh, meshFilter.mesh);
        }

        [Test]
        public async Task MeshNode_Has_MeshRenderer_With_Default_Material()
        {
            var meshNode = await ImportMeshNode(0, meshCache);

            var meshRenderer = meshNode.GetComponent<MeshRenderer>();
            Assert.True(meshRenderer, "Has mesh-renderer");

            Assert.True(meshRenderer.material);
        }
    }
}