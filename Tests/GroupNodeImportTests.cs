using System;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using static At.Ac.FhStp.Import3D.AssimpNodeBuilding;
using static At.Ac.FhStp.Import3D.Nodes.Import;

namespace At.Ac.FhStp.Import3D
{

    [RequiresPlayMode]
    public class GroupNodeImportTests
    {

        private readonly Mesh mesh0;
        private readonly Mesh mesh1;
        private readonly MeshCache meshCache;


        public GroupNodeImportTests()
        {
            mesh0 = new Mesh { name = "Mesh 0" };
            mesh1 = new Mesh { name = "Mesh 1" };
            meshCache = new MeshCache(i =>
            {
                return i switch
                {
                    0 => Task.FromResult(mesh0),
                    1 => Task.FromResult(mesh1),
                    _ => throw new ArgumentException()
                };
            });
        }


        [Test]
        public async Task GroupNode_Name_Is_Assimp_Name()
        {
            var assimpNode = MakeLeaf("My Root");

            var node = await ImportNode(assimpNode, meshCache);

            Assert.AreEqual(assimpNode.Name, node.name);
        }

        [Test]
        public async Task GroupNode_Tree_Has_Same_Structure_As_Assimp()
        {
            var assimpNode =
                MakeTree("A", new[]
                {
                    MakeLeaf("B"),
                    MakeTree("C", new[]
                    {
                        MakeLeaf("D")
                    })
                });

            var a = await ImportNode(assimpNode, meshCache);
            Assert.True(a, "A");
            AssertChildCount(a, 2);
            AssertForChild(a, 1, c => AssertChildCount(c, 1));
        }

        [Test]
        public async Task GroupNode_Tree_Has_Same_MeshNodes_As_Assimp()
        {
            var assimpNode =
                MakeTree("A", new[]
                {
                    0
                }, new[]
                {
                    MakeLeaf("B", new[]
                    {
                        0, 1
                    })
                });

            var a = await ImportNode(assimpNode, meshCache);
            Assert.True(a, "A");
            AssertChildCount(a, 2);

            AssertForChild(a, 1, meshNodeA0 =>
                               AssertIsMeshNodeWith(meshNodeA0, mesh0));
            AssertForChild(a, 0, b =>
            {
                AssertChildCount(b, 2);
                AssertForChild(b, 0, meshNodeB0 =>
                                   AssertIsMeshNodeWith(meshNodeB0, mesh0));
                AssertForChild(b, 1, meshNodeB1 =>
                                   AssertIsMeshNodeWith(meshNodeB1, mesh1));
            });
        }

        private static void AssertForChild(
            GameObject g, int index, Action<GameObject> assert)
        {
            var child = g.transform.GetChild(index);
            Assert.True(child, $"{g.name} has child at {index}");
            assert(child.gameObject);
        }

        private static void AssertChildCount(GameObject g, int count) =>
            Assert.AreEqual(count, g.transform.childCount, $"{g.name} child-count");

        private static void AssertIsMeshNodeWith(GameObject g, Mesh mesh)
        {
            var meshFilter = g.GetComponent<MeshFilter>();
            Assert.True(meshFilter, $"{g.name} is mesh-node");
            Assert.AreEqual(mesh, meshFilter.mesh);
        }

    }

}