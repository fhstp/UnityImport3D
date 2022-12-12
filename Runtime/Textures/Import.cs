using System;
using System.Threading.Tasks;
using UnityEngine;
using AssimpTexture = Assimp.EmbeddedTexture;
using static At.Ac.FhStp.Import3D.Textures.ModelConversion;
using static At.Ac.FhStp.Import3D.Textures.Instantiation;
using static At.Ac.FhStp.Import3D.TaskManagement;
using static At.Ac.FhStp.Import3D.Common.DataCopy;
using static At.Ac.FhStp.Import3D.Textures.DataCopy;

namespace At.Ac.FhStp.Import3D.Textures
{

    internal static class Import
    {

        internal static async Task<Texture2D> ImportTexture(AssimpTexture assimpTexture)
        {
            var model = await InBackground(() => ConvertToModel(assimpTexture));
            switch (model)
            {
                case CompressedTextureModel compressed:
                {
                    var texture = await MakeEmptyTexture2D();
                    await InParallel(
                        CopyName(model, texture),
                        CopyTextureBytes(compressed, texture));
                    return texture;
                }
                case NonCompressedTextureModel nonCompressed:
                {
                    var texture = await MakeTexture2DWithSize(
                        nonCompressed.Width, nonCompressed.Height);
                    await InParallel(
                        CopyName(model, texture),
                        CopyTexturePixels(nonCompressed, texture));
                    return texture;
                }
                default: throw new ArgumentException("Unknown texture type!");
            }
        }

    }

}