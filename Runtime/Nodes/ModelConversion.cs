using System.Collections.Immutable;
using System.Linq;
using UnityEngine;
using static At.Ac.FhStp.Import3D.Conversion;
using AssimpNode = Assimp.Node;

namespace At.Ac.FhStp.Import3D.Nodes
{
    internal static class ModelConversion
    {
        private static bool IsXInverted(Matrix4x4 matrix) =>
            Vector3.Cross(matrix.GetColumn(0), matrix.GetColumn(1)).normalized != (Vector3)matrix.GetColumn(2).normalized;

        private static Vector3 PositionIn(Matrix4x4 matrix) =>
            new Vector3(matrix.m03,
                matrix.m13,
                matrix.m23);

        private static Quaternion RotationIn(Matrix4x4 matrix)
        {
            var forward = new Vector3(
                matrix.m02,
                matrix.m12,
                matrix.m22);

            var upwards = new Vector3(
                matrix.m01,
                matrix.m11,
                matrix.m21);

            return Quaternion.LookRotation(forward, upwards);
        }

        private static Vector3 ScaleIn(this Matrix4x4 matrix)
        {
            var scale = new Vector3(
                matrix.GetColumn(0).magnitude,
                matrix.GetColumn(1).magnitude,
                matrix.GetColumn(2).magnitude);

            if (IsXInverted(matrix)) scale.x *= -1;

            return scale;
        }

        private static Matrix4x4 ConvertMatrix(Assimp.Matrix4x4 matrix) =>
            new Matrix4x4(
                new Vector4(matrix.A1, matrix.B1, matrix.C1, matrix.D1),
                new Vector4(matrix.A2, matrix.B2, matrix.C2, matrix.D2),
                new Vector4(matrix.A3, matrix.B3, matrix.C3, matrix.D3),
                new Vector4(matrix.A4, matrix.B4, matrix.C4, matrix.D4));
        
        internal static GroupNodeModel ConvertToModel(AssimpNode assimpNode)
        {
            var children = assimpNode.Children
                .Select(ConvertToModel)
                .ToImmutableArray();
            var meshIndices = assimpNode.MeshIndices.ToImmutableArray();
            var matrix = ConvertMatrix(assimpNode.Transform);
            var position = PositionIn(matrix);
            var rotation = RotationIn(matrix);
            var scale = ScaleIn(matrix);
            
            return new GroupNodeModel(
                assimpNode.Name, children, meshIndices, position, rotation, scale);
        }
    }
}