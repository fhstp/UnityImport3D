using System.Collections.Immutable;
using UnityEngine;

namespace At.Ac.FhStp.Import3D.Nodes
{
    internal class GroupNodeModel : INamedModel
    {
        public GroupNodeModel(
            string name, ImmutableArray<GroupNodeModel> children,
            ImmutableArray<int> meshIndices, Vector3 position)
        {
            Name = name;
            Children = children;
            MeshIndices = meshIndices;
            Position = position;
        }

        public ImmutableArray<GroupNodeModel> Children { get; }

        public ImmutableArray<int> MeshIndices { get; }
        
        public Vector3 Position { get; }

        public string Name { get; }
    }
}