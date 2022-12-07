using System.Collections.Immutable;
using System.IO;
using AssimpEmbeddedTexture = Assimp.EmbeddedTexture;

namespace At.Ac.FhStp.Import3D
{

    internal static class AssimpToModel
    {

        private static string FileNameOf(AssimpEmbeddedTexture assimpTexture) =>
            Path.GetFileNameWithoutExtension(assimpTexture.Filename);

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