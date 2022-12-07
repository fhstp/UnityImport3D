using System.Collections.Immutable;
using System.IO;
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

        internal static Conversion<Color> Texel(AssimpTexel texel) =>
            Conversion<Color>.Const(new Color(NormalizeByte01(texel.R),
                                              NormalizeByte01(texel.G),
                                              NormalizeByte01(texel.B),
                                              NormalizeByte01(texel.A)));

        internal static Conversion<TextureModel> EmbeddedTexture(AssimpEmbeddedTexture assimpTexture) =>
            Conversion<TextureModel>.Sync(() =>
            {
                // TODO: Check if texture is compressed
                // TODO: Use format hint to determine if texture type is supported
                var name = FileNameOf(assimpTexture);
                var bytes = assimpTexture.CompressedData.ToImmutableArray();
                return new CompressedTextureModel(name, bytes);
            });

    }

}