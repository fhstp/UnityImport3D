using System;
using UnityEngine;

namespace At.Ac.FhStp.Import3D.Meshes
{
    internal class MeshModel : INamedModel
    {
        internal MeshModel(
            string name, Lazy<Vector3[]> vertices, Lazy<int[]> triangles, Lazy<Vector3[]> normals)
        {
            Name = name;
            Vertices = vertices;
            Triangles = triangles;
            Normals = normals;
        }

        internal Lazy<Vector3[]> Vertices { get; }

        internal Lazy<int[]> Triangles { get; }

        internal Lazy<Vector3[]> Normals { get; }

        public string Name { get; }
    }
}