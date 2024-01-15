using System.Collections.Immutable;
using UnityEngine;

namespace At.Ac.FhStp.Import3D.Textures
{
    internal abstract class TextureModel : INamedModel
    {
        protected TextureModel(string name) =>
            Name = name;

        public string Name { get; }
    }

    internal class CompressedTextureModel : TextureModel
    {
        public CompressedTextureModel(string name, ImmutableArray<byte> bytes)
            : base(name) => Bytes = bytes;

        internal ImmutableArray<byte> Bytes { get; }
    }

    internal class NonCompressedTextureModel : TextureModel
    {
        public NonCompressedTextureModel(string name, int width, int height, ImmutableArray<Color> pixels)
            : base(name)
        {
            Width = width;
            Height = height;
            Pixels = pixels;
        }

        public int Width { get; }

        public int Height { get; }

        public ImmutableArray<Color> Pixels { get; }
    }
}