using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using AssimpEmbeddedTexture = Assimp.EmbeddedTexture;
using AssimpTexel = Assimp.Texel;

namespace At.Ac.FhStp.Import3D
{

    [RequiresPlayMode(false)]
    public class EmbeddedTextureToModelTests
    {

        private static AssimpTexel[] MakeBlackTexels(int width, int height) =>
            Enumerable.Repeat(new AssimpTexel(0, 0, 0, 1), width * height)
                      .ToArray();

        private static AssimpEmbeddedTexture MakeBlackTexture(
            int width, int height, string name) =>
            new AssimpEmbeddedTexture(width, height,
                                      MakeBlackTexels(width, height), name);

        [Test]
        public void CompressedTexture_Model_Name_Is_Assimp_Filename_Without_Extension()
        {
            const string fileNameWithoutExtension = "file";
            var fileName = $"{fileNameWithoutExtension}.png";
            var assimpTexture = new AssimpEmbeddedTexture(
                "png", new byte[] { 0x1, 0x2, 0x3, 0x4 }, fileName);

            var model = AssimpToModel.EmbeddedTexture(assimpTexture);

            if (model is CompressedTextureModel compressed)
                Assert.AreEqual(fileNameWithoutExtension, compressed.Name);
            else
                Assert.Fail("Model is not a compressed texture!");
        }

        [Test]
        public void CompressedTexture_Model_Bytes_Are_Assimp_Bytes()
        {
            var bytes = new byte[] { 0x1, 0x2, 0x3, 0x4 };
            var assimpTexture = new AssimpEmbeddedTexture(
                "png", bytes, "file.png");

            var model = AssimpToModel.EmbeddedTexture(assimpTexture);

            if (model is CompressedTextureModel compressed)
                Assert.AreEqual(bytes, compressed.Bytes);
            else
                Assert.Fail("Model is not a compressed texture!");
        }


        [Test]
        public void NonCompressedTexture_Model_Name_Is_Assimp_Filename_Without_Extension()
        {
            const string fileNameWithoutExtension = "file";
            var fileName = $"{fileNameWithoutExtension}.png";
            var assimpTexture = MakeBlackTexture(1, 1, fileName);

            var model = AssimpToModel.EmbeddedTexture(assimpTexture);

            if (model is NonCompressedTextureModel nonCompressed)
                Assert.AreEqual(fileNameWithoutExtension, nonCompressed.Name);
            else
                Assert.Fail("Model is not a non-compressed texture!");
        }

        [Test]
        public void NonCompressedTexture_Model_Width_Is_Assimp_Width()
        {
            const int width = 2;
            var assimpTexture = MakeBlackTexture(width, 1, "file.png");

            var model = AssimpToModel.EmbeddedTexture(assimpTexture);

            if (model is NonCompressedTextureModel nonCompressed)
                Assert.AreEqual(width, nonCompressed.Width);
            else
                Assert.Fail("Model is not a non-compressed texture!");
        }

        [Test]
        public void NonCompressedTexture_Model_Height_Is_Assimp_Height()
        {
            const int height = 2;
            var assimpTexture = MakeBlackTexture(1, height, "file.png");

            var model = AssimpToModel.EmbeddedTexture(assimpTexture);

            if (model is NonCompressedTextureModel nonCompressed)
                Assert.AreEqual(height, nonCompressed.Height);
            else
                Assert.Fail("Model is not a non-compressed texture!");
        }

        [Test]
        public void NonCompressedTexture_Model_Pixels_Are_Assimp_Texels_As_Colors()
        {
            const byte r = 10;
            const byte g = 20;
            const byte b = 30;
            const byte a = 40;
            var texels = new[] { new AssimpTexel(b, g, r, a) };
            var pixels = new[] { new Color(r / 255f, g / 255f, b / 255f, a / 255f) };
            var assimpTexture = new AssimpEmbeddedTexture(1, 1, texels, "file.png");

            var model = AssimpToModel.EmbeddedTexture(assimpTexture);

            if (model is NonCompressedTextureModel nonCompressed)
                Assert.AreEqual(pixels, nonCompressed.Pixels);
            else
                Assert.Fail("Model is not a non-compressed texture!");
        }

    }

}