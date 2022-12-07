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

        internal static Conversion<Color> Texel(AssimpTexel texel) =>
            Conversion<Color>.Const(new Color(NormalizeByte01(texel.R),
                                              NormalizeByte01(texel.G),
                                              NormalizeByte01(texel.B),
                                              NormalizeByte01(texel.A)));

        private static Conversion<TextureModel> CompressedTexture(
            AssimpEmbeddedTexture assimpTexture) =>
            Conversion<TextureModel>.Sync(() =>
            {
                var name = FileNameOf(assimpTexture);

                // TODO: Use format hint to determine if texture type is supported
                var bytes = assimpTexture.CompressedData.ToImmutableArray();
                return new CompressedTextureModel(name, bytes);
            });

        private static Conversion<TextureModel> NonCompressedTexture(
            AssimpEmbeddedTexture assimpTexture) =>
            Conversion<TextureModel>.Async(async () =>
            {
                var name = FileNameOf(assimpTexture);

                var width = assimpTexture.Width;
                var height = assimpTexture.Height;
                var pixels = await assimpTexture.NonCompressedData
                                                .Select(Texel)
                                                .InParallel();
                return new NonCompressedTextureModel(name, width, height,
                                                     pixels.ToImmutableArray());
            });

        internal static Conversion<TextureModel> EmbeddedTexture(
            AssimpEmbeddedTexture assimpTexture) =>
            Conversion<TextureModel>.Async(
                async () => assimpTexture.IsCompressed
                    ? await CompressedTexture(assimpTexture)
                    : await NonCompressedTexture(assimpTexture));

    }

}