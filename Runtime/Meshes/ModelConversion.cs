using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static At.Ac.FhStp.Import3D.Conversion;
using AssimpMesh = Assimp.Mesh;
using AssimpVector = Assimp.Vector3D;
using AssimpFace = Assimp.Face;

namespace At.Ac.FhStp.Import3D.Meshes
{
    internal static class ModelConversion
    {
        private static IEnumerable<int> FaceTriangles(AssimpFace face) =>
            face.Indices;

        internal static MeshModel ConvertToModel(AssimpMesh assimpMesh, float scalingFactor = 1)
        {
            var vertices = new Lazy<Vector3[]>(
                () => assimpMesh.Vertices
                                .Select(ConvertVector)
                                .Select(v => v * scalingFactor)
                                .ToArray());

            var triangles = new Lazy<int[]>(
                () => assimpMesh.Faces.SelectMany(FaceTriangles).ToArray());

            var normals = new Lazy<Vector3[]>(
                () => assimpMesh.Normals.Select(ConvertVector).ToArray());

            return new MeshModel(assimpMesh.Name, vertices, triangles, normals);
        }
    }
}