using System.Collections.Immutable;

namespace At.Ac.FhStp.Import3D.Nodes
{
    internal class GroupNodeModel : INamedModel
    {
        public GroupNodeModel(
            string name, ImmutableArray<GroupNodeModel> children,
            ImmutableArray<int> meshIndices)
        {
            Name = name;
            Children = children;
            MeshIndices = meshIndices;
        }

        public ImmutableArray<GroupNodeModel> Children { get; }

        public ImmutableArray<int> MeshIndices { get; }

        public string Name { get; }
    }
}