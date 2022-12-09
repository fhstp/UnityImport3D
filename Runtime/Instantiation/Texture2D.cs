using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev.ComradeVanti;
using UnityEngine;
using static At.Ac.FhStp.Import3D.TaskManagement;

namespace At.Ac.FhStp.Import3D
{

    internal static partial class Instantiate
    {

        private static Task<Texture2D> MakeEmptyTexture2D() =>
            Task.FromResult(new Texture2D(0, 0));

        private static Task<Nothing> WriteBytes(Texture2D texture, IEnumerable<byte> bytes)
        {
            texture.LoadImage(bytes.ToArray());
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
                default: throw new ArgumentException("Unknown texture type!");
            }
        }

    }

}