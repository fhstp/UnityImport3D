using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev.ComradeVanti;
using UnityEngine;

namespace At.Ac.FhStp.Import3D
{

    internal static partial class Instantiate
    {

        private static Func<Texture2D> MakeEmptyTexture2D() =>
            () => new Texture2D(0, 0);

        private static Func<Nothing> WriteBytes(
            Texture2D texture, IEnumerable<byte> bytes) =>
            () =>
            {
                texture.LoadImage(bytes.ToArray());
                texture.Apply();
                return Nothing.atAll;
            };

        internal static async Task<Texture2D> Texture2D(TextureModel model, ITaskRunner api)
        {
            switch (model)
            {
                case CompressedTextureModel compressed:
                {
                    var texture = await api.Run(MakeEmptyTexture2D());
                    await api.RunInParallel(
                        SetName(texture, model.Name),
                        WriteBytes(texture, compressed.Bytes));
                    return texture;
                }
                default: throw new ArgumentException("Unknown texture type!");
            }
        }

    }

}