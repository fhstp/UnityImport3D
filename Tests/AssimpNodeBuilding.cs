using System.Collections.Generic;
using System.Linq;
using AssimpNode = Assimp.Node;

namespace At.Ac.FhStp.Import3D
{
    internal static class AssimpNodeBuilding
    {
        internal static AssimpNode MakeTree(
            string name,
            IEnumerable<int> meshIndices,
            IEnumerable<AssimpNode> children)
        {
            var node = new AssimpNode(name);
            foreach (var child in children) node.Children.Add(child);
            node.MeshIndices.AddRange(meshIndices);
            return node;
        }

        internal static AssimpNode MakeTree(
            string name,
            IEnumerable<AssimpNode> children) =>
            MakeTree(name, Enumerable.Empty<int>(), children);

        internal static AssimpNode MakeLeaf(string name, IEnumerable<int> meshIndices) =>
            MakeTree(name, meshIndices, Enumerable.Empty<AssimpNode>());

        internal static AssimpNode MakeLeaf(string name) =>
            MakeLeaf(name, Enumerable.Empty<int>());
    }
}