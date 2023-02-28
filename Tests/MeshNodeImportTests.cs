using System.Threading.Tasks;
using At.Ac.FhStp.Import3D.Nodes;
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
        private readonly Material material;
        private readonly MeshCache meshCache;
        private readonly MaterialCache materialCache;


        public MeshNodeImportTests()
        {
            mesh = new Mesh
            {
                name = "My Mesh"
            };
            material = new Material(Shader.Find("Standard"));
            meshCache = new MeshCache(_ => Task.FromResult(mesh));
            materialCache = new MaterialCache(_ => Task.FromResult(material));
        }


        [Test]
        public async Task MeshNode_Name_Is_Mesh_Name()
        {
            var meshNode = new MeshNode(0, 0);
            var gameObject = await ImportMeshNode(meshNode, meshCache, materialCache);

            Assert.AreEqual(mesh.name, gameObject.name);
        }

        [Test]
        public async Task MeshNode_Has_MeshFilter_With_Mesh()
        {
            var meshNode = new MeshNode(0, 0);
            var gameObject = await ImportMeshNode(meshNode, meshCache, materialCache);

            var meshFilter = gameObject.GetComponent<MeshFilter>();
            Assert.True(meshFilter, "Has mesh-filter");

            Assert.AreEqual(mesh, meshFilter.mesh);
        }

        [Test]
        public async Task MeshNode_Has_MeshRenderer_With_Default_Material()
        {
            var meshNode = new MeshNode(0, 0);
            var gameObject = await ImportMeshNode(meshNode, meshCache, materialCache);

            var meshRenderer = gameObject.GetComponent<MeshRenderer>();
            Assert.True(meshRenderer, "Has mesh-renderer");

            Assert.True(meshRenderer.material);
        }
    }
}