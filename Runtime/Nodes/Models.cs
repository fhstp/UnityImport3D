using System.Collections.Immutable;

namespace At.Ac.FhStp.Import3D.Nodes
{

    internal class GroupNodeModel : INamedModel
    {

        public ImmutableArray<GroupNodeModel> Children { get; }
        
        public ImmutableArray<int> MeshIndices { get; }

        public GroupNodeModel(
            string name, ImmutableArray<GroupNodeModel> children,
            ImmutableArray<int> meshIndices)
        {
            Name = name;
            Children = children;
            MeshIndices = meshIndices;
        }

        public string Name { get; }

    }

}