using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace At.Ac.FhStp.Import3D
{

    [RequiresPlayMode]
    public class InstantiateTexture2DTests
    {

        private static IEnumerable<Color> MakeSolidPixels(int width, int height, Color color) =>
            Enumerable.Repeat(color, width * height);

        private static ImmutableArray<byte> MakePngBytes(
            int width, int height, Color color)
        {
            var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
            var pixels = MakeSolidPixels(width, height, color).ToArray();
            tex.SetPixels(pixels);
            var bytes = tex.EncodeToPNG().ToImmutableArray();
            Object.Destroy(tex);
            return bytes;
        }

        [Test]
        public async Task CompressedTexture_Name_Is_Model_Name()
        {
            const string name = "image";

            var model = new CompressedTextureModel(name, ImmutableArray<byte>.Empty);
            var tex = await Instantiate.Texture2D(model);

            Assert.AreEqual(name, tex.name);
        }

        [Test]
        public async Task CompressedTexture_Size_Comes_From_Model_Bytes()
        {
            const int width = 2;
            const int height = 4;
            var bytes = MakePngBytes(width, height, Color.black);

            var model = new CompressedTextureModel("image", bytes);
            var tex = await Instantiate.Texture2D(model);

            Assert.AreEqual(width, tex.width);
            Assert.AreEqual(height, tex.height);
        }

        [Test]
        public async Task CompressedTexture_Pixels_Come_From_Model_Bytes()
        {
            const int width = 1;
            const int height = 1;
            var bytes = MakePngBytes(width, height, Color.green);

            var model = new CompressedTextureModel("image", bytes);
            var tex = await Instantiate.Texture2D(model);

            Assert.AreEqual(Color.green, tex.GetPixel(0, 0));
        }

        [Test]
        public async Task NonCompressedTexture_Name_Is_Model_Name()
        {
            const string name = "image";

            var model = new NonCompressedTextureModel(
                name, 0, 0, ImmutableArray<Color>.Empty);
            var tex = await Instantiate.Texture2D(model);

            Assert.AreEqual(name, tex.name);
        }

        [Test]
        public async Task NonCompressedTexture_Size_Is_Model_Size()
        {
            const int width = 2;
            const int height = 4;

            var model = new NonCompressedTextureModel(
                "image", width, height,
                MakeSolidPixels(width, height, Color.black)
                    .ToImmutableArray());
            var tex = await Instantiate.Texture2D(model);

            Assert.AreEqual(width, tex.width);
            Assert.AreEqual(height, tex.height);
        }

        [Test]
        public async Task NonCompressedTexture_Pixels_Is_Model_Pixels()
        {
            const int width = 2;
            const int height = 4;
            var pixels = MakeSolidPixels(width, height, Color.black)
                .ToImmutableArray();
            var model = new NonCompressedTextureModel(
                "image", width, height, pixels);
            var tex = await Instantiate.Texture2D(model);

            Assert.AreEqual(pixels, tex.GetPixels());
        }

    }

}