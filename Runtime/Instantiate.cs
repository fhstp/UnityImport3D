using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev.ComradeVanti;
using UnityEngine;
using UnityObject = UnityEngine.Object;
using static At.Ac.FhStp.Import3D.TaskManagement;

namespace At.Ac.FhStp.Import3D
{

    internal static class Instantiate
    {
        
        private static Task<Nothing> SetName(UnityObject o, string name)
        {
            o.name = name;
            return noResult;
        }

        private static Task<Texture2D> MakeTexture2DWithSize(
            int width, int height) =>
            Task.FromResult(new Texture2D(width, height));

        private static Task<Texture2D> MakeEmptyTexture2D() =>
            MakeTexture2DWithSize(0, 0);

        private static Task<Nothing> WriteBytes(Texture2D texture, IEnumerable<byte> bytes)
        {
            texture.LoadImage(bytes.ToArray());
            return noResult;
        }

        private static Task<Nothing> WritePixels(Texture2D texture, IEnumerable<Color> pixels)
        {
            texture.SetPixels(pixels.ToArray());
            texture.Apply();
            return noResult;
        }

        internal static async Task<Texture2D> Texture2D(TextureModel model)
        {
            switch (model)
            {
                case CompressedTextureModel compressed:
                {
                    var texture = await MakeEmptyTexture2D();
                    await InParallel(
                        SetName(texture, model.Name),
                        WriteBytes(texture, compressed.Bytes));
                    return texture;
                }
                case NonCompressedTextureModel nonCompressed:
                {
                    var texture = await MakeTexture2DWithSize(
                        nonCompressed.Width, nonCompressed.Height);
                    await InParallel(
                        SetName(texture, model.Name),
                        WritePixels(texture, nonCompressed.Pixels));
                    return texture;
                }
                default: throw new ArgumentException("Unknown texture type!");
            }
        }

    }

}