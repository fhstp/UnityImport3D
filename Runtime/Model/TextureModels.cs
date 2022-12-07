using System.Collections.Immutable;

namespace At.Ac.FhStp.Import3D
{

    internal abstract class TextureModel
    {

        internal string Name { get; }


        protected TextureModel(string name) =>
            Name = name;

    }

    internal class CompressedTextureModel : TextureModel
    {

        internal ImmutableArray<byte> Bytes { get; }


        public CompressedTextureModel(string name, ImmutableArray<byte> bytes)
            : base(name) => Bytes = bytes;

    }

}