using System.Collections.Immutable;
using System.Linq;
using UnityEngine;
using static At.Ac.FhStp.Import3D.Conversion;
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
            assimpNode.Transform.Decompose(
                out _, 
                out _, 
                out var assimpPosition);

            var position = ConvertVector(assimpPosition);
            
            return new GroupNodeModel(
                assimpNode.Name, children, meshIndices, position);
        }
    }
}