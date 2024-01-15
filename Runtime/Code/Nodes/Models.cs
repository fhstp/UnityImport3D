using System.Collections.Immutable;
using UnityEngine;

namespace At.Ac.FhStp.Import3D.Nodes
{

    internal class MeshNode
    {
        
        public int MeshIndex { get; }
        
        public int MaterialIndex { get; }


        public MeshNode(int meshIndex, int materialIndex)
        {
            MeshIndex = meshIndex;
            MaterialIndex = materialIndex;
        }
    }
    
    internal class GroupNodeModel : INamedModel
    {
        public GroupNodeModel(
            string name, 
            ImmutableArray<GroupNodeModel> children,
            ImmutableArray<MeshNode> meshNodes, 
            Vector3 position, Quaternion rotation, Vector3 scale)
        {
            Name = name;
            Children = children;
            MeshNodes = meshNodes;
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }

        public ImmutableArray<GroupNodeModel> Children { get; }

        public ImmutableArray<MeshNode> MeshNodes { get; }
        
        public Vector3 Position { get; }
        
        public Quaternion Rotation { get; }
        
        public Vector3 Scale { get; }

        public string Name { get; }
    }
}