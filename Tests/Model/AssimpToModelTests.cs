using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using AssimpEmbeddedTexture = Assimp.EmbeddedTexture;
using AssimpTexel = Assimp.Texel;

namespace At.Ac.FhStp.Import3D
{

    [RequiresPlayMode(false)]
    public class AssimpToModelTests
    {

        [Test]
        public async Task Pixel_Color_R_Is_Assimp_Texel_R_Normalized_To_01()
        {
            const byte r = 100;
            const float rNormalized = r / 255f;
            var texel = new AssimpTexel(0, 0, r, 0);

            var pixel = await AssimpToModel.Texel(texel);

            Assert.AreEqual(rNormalized, pixel.r);
        }
        
        [Test]
        public async Task Pixel_Color_G_Is_Assimp_Texel_G_Normalized_To_01()
        {
            const byte g = 100;
            const float gNormalized = g / 255f;
            var texel = new AssimpTexel(0, g, 0, 0);

            var pixel = await AssimpToModel.Texel(texel);

            Assert.AreEqual(gNormalized, pixel.g);
        }
        
        [Test]
        public async Task Pixel_Color_B_Is_Assimp_Texel_B_Normalized_To_01()
        {
            const byte b = 100;
            const float bNormalized = b / 255f;
            var texel = new AssimpTexel(b, 0, 0, 0);

            var pixel = await AssimpToModel.Texel(texel);

            Assert.AreEqual(bNormalized, pixel.b);
        }
        
        [Test]
        public async Task Pixel_Color_A_Is_Assimp_Texel_A_Normalized_To_01()
        {
            const byte a = 100;
            const float aNormalized = a / 255f;
            var texel = new AssimpTexel(0, 0, 0, a);

            var pixel = await AssimpToModel.Texel(texel);

            Assert.AreEqual(aNormalized, pixel.a);
        }

        [Test]
        public async Task CompressedTexture_Model_Name_Is_Assimp_Filename_Without_Extension()
        {
            const string fileNameWithoutExtension = "file";
            var fileName = $"{fileNameWithoutExtension}.png";
            var assimpTexture = new AssimpEmbeddedTexture(
                "png", new byte[] { 0x1, 0x2, 0x3, 0x4 }, fileName);

            var model = await AssimpToModel.EmbeddedTexture(assimpTexture);

            if (model is CompressedTextureModel compressed)
                Assert.AreEqual(fileNameWithoutExtension,
                                compressed.Name);
            else
                Assert.Fail("Model is not a compressed texture!");
        }

        [Test]
        public async Task CompressedTexture_Model_Bytes_Are_Assimp_Bytes()
        {
            var bytes = new byte[] { 0x1, 0x2, 0x3, 0x4 };
            var assimpTexture = new AssimpEmbeddedTexture(
                "png", bytes, "file.png");

            var model = await AssimpToModel.EmbeddedTexture(assimpTexture);

            if (model is CompressedTextureModel compressed)
                Assert.AreEqual(bytes, compressed.Bytes);
            else
                Assert.Fail("Model is not a compressed texture!");
        }

    }

}