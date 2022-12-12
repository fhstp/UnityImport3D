using System.Collections.Immutable;
using UnityEngine;

namespace At.Ac.FhStp.Import3D
{

    internal abstract class TextureModel : INamedModel
    {

        public string Name { get; }


        protected TextureModel(string name) =>
            Name = name;

    }

    internal class CompressedTextureModel : TextureModel
    {

        internal ImmutableArray<byte> Bytes { get; }


        public CompressedTextureModel(string name, ImmutableArray<byte> bytes)
            : base(name) => Bytes = bytes;

    }

    internal class NonCompressedTextureModel : TextureModel
    {

        public int Width { get; }

        public int Height { get; }
        
        public  ImmutableArray<Color> Pixels { get; }


        public NonCompressedTextureModel(string name, int width, int height, ImmutableArray<Color> pixels)
            : base(name)
        {
            Width = width;
            Height = height;
            Pixels = pixels;
        }

    }

}