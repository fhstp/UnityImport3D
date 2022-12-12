using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityObject = UnityEngine.Object;
using static At.Ac.FhStp.Import3D.TaskManagement;
using static At.Ac.FhStp.Import3D.DataCopy.Common;
using static At.Ac.FhStp.Import3D.DataCopy.Texture2D;

namespace At.Ac.FhStp.Import3D
{

    internal static class Instantiate
    {

        private static Task<Texture2D> MakeTexture2DWithSize(
            int width, int height) =>
            Task.FromResult(new Texture2D(width, height));

        private static Task<Texture2D> MakeEmptyTexture2D() =>
            MakeTexture2DWithSize(0, 0);

        internal static async Task<Texture2D> Texture2D(TextureModel model)
        {
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