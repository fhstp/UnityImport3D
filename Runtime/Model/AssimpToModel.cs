using System.Collections.Immutable;
using System.IO;
using System.Linq;
using UnityEngine;
using AssimpEmbeddedTexture = Assimp.EmbeddedTexture;
using AssimpTexel = Assimp.Texel;

namespace At.Ac.FhStp.Import3D
{

    internal static class AssimpToModel
    {

        private static string FileNameOf(AssimpEmbeddedTexture assimpTexture) =>
            Path.GetFileNameWithoutExtension(assimpTexture.Filename);

        private static float NormalizeByte01(byte b) =>
            b / 255f;

        internal static Color Texel(AssimpTexel texel) =>
            new Color(NormalizeByte01(texel.R),
                      NormalizeByte01(texel.G),
                      NormalizeByte01(texel.B),
                      NormalizeByte01(texel.A));

        private static TextureModel CompressedTexture(AssimpEmbeddedTexture assimpTexture)
        {
            var name = FileNameOf(assimpTexture);

            // TODO: Use format hint to determine if texture type is supported
            var bytes = assimpTexture.CompressedData.ToImmutableArray();
            return new CompressedTextureModel(name, bytes);
        }

        private static TextureModel NonCompressedTexture(AssimpEmbeddedTexture assimpTexture)
        {
            var name = FileNameOf(assimpTexture);

            var width = assimpTexture.Width;
            var height = assimpTexture.Height;
            var pixels = assimpTexture.NonCompressedData
                                      .Select(Texel)
                                      .ToImmutableArray();
            return new NonCompressedTextureModel(name, width, height, pixels);
        }

        internal static TextureModel EmbeddedTexture(AssimpEmbeddedTexture assimpTexture) =>
            assimpTexture.IsCompressed
                ? CompressedTexture(assimpTexture)
                : NonCompressedTexture(assimpTexture);

    }

}