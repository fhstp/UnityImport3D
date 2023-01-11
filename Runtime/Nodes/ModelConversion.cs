using System.Collections.Immutable;
using System.Linq;
using AssimpNode = Assimp.Node;

namespace At.Ac.FhStp.Import3D.Nodes
{
    internal static class ModelConversion
    {
        internal static GroupNodeModel ConvertToModel(AssimpNode assimpNode)
        {
            var children = assimpNode.Children
                .Select(ConvertToModel)
                .ToImmutableArray();
            var meshIndices = assimpNode.MeshIndices.ToImmutableArray();

            return new GroupNodeModel(assimpNode.Name, children, meshIndices);
        }
    }
}