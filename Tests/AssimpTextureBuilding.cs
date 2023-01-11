using System.Linq;
using UnityEngine;
using AssimpTexel = Assimp.Texel;
using AssimpEmbeddedTexture = Assimp.EmbeddedTexture;

namespace At.Ac.FhStp.Import3D
{
    internal static class AssimpTextureBuilding
    {
        private static AssimpTexel[] MakeSolidTexels(int width, int height, AssimpTexel texel) =>
            Enumerable.Repeat(texel, width * height).ToArray();

        private static Color[] MakeSolidPixels(int width, int height, Color color) =>
            Enumerable.Repeat(color, width * height).ToArray();

        private static byte[] MakePngBytes(
            int width, int height, Color color)
        {
            var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
            var pixels = MakeSolidPixels(width, height, color);
            tex.SetPixels(pixels);
            var bytes = tex.EncodeToPNG();
            Object.Destroy(tex);
            return bytes;
        }

        internal static AssimpEmbeddedTexture MakeSolidColorTexture(string name, int width, int height,
            AssimpTexel texel)
        {
            var texels = MakeSolidTexels(width, height, texel);
            return new AssimpEmbeddedTexture(width, height, texels, name);
        }

        internal static AssimpEmbeddedTexture MakeSolidBlackTexture(string name, int width, int height) =>
            MakeSolidColorTexture(name, width, height, new AssimpTexel(0, 0, 0, 255));

        internal static AssimpEmbeddedTexture MakePngFromBytes(string name, byte[] bytes) => new("png", bytes, name);

        internal static AssimpEmbeddedTexture MakeColorPng(
            string name, int width, int height, Color color)
        {
            var bytes = MakePngBytes(width, height, color);
            return MakePngFromBytes(name, bytes);
        }

        internal static AssimpEmbeddedTexture Make1x1Png(string name, Color color) =>
            MakeColorPng(name, 1, 1, color);
    }
}