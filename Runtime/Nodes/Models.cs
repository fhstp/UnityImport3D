using System.Collections.Immutable;
using UnityEngine;

namespace At.Ac.FhStp.Import3D.Nodes
{
    internal class GroupNodeModel : INamedModel
    {
        public GroupNodeModel(
            string name, ImmutableArray<GroupNodeModel> children,
            ImmutableArray<int> meshIndices, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            Name = name;
            Children = children;
            MeshIndices = meshIndices;
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }

        public ImmutableArray<GroupNodeModel> Children { get; }

        public ImmutableArray<int> MeshIndices { get; }
        
        public Vector3 Position { get; }
        
        public Quaternion Rotation { get; }
        
        public Vector3 Scale { get; }

        public string Name { get; }
    }
}