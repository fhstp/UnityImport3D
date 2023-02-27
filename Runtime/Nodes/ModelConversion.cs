using System.Collections.Immutable;
using System.Linq;
using UnityEngine;
using static At.Ac.FhStp.Import3D.Conversion;
using AssimpNode = Assimp.Node;

namespace At.Ac.FhStp.Import3D.Nodes
{
    internal static class ModelConversion
    {

        private static Quaternion ConvertQuaternion(Assimp.Quaternion quaternion) => 
            new Quaternion(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);

        internal static GroupNodeModel ConvertToModel(AssimpNode assimpNode)
        {
            var children = assimpNode.Children
                .Select(ConvertToModel)
                .ToImmutableArray();
            var meshIndices = assimpNode.MeshIndices.ToImmutableArray();
            assimpNode.Transform.Decompose(
                out _, 
                out var assimpRotation, 
                out var assimpPosition);

            var position = ConvertVector(assimpPosition);
            var rotation = ConvertQuaternion(assimpRotation);
            
            return new GroupNodeModel(
                assimpNode.Name, children, meshIndices, position, rotation);
        }
    }
}