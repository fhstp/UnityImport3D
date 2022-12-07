using System.Threading.Tasks;
using NUnit.Framework;
using AssimpEmbeddedTexture = Assimp.EmbeddedTexture;

namespace At.Ac.FhStp.Import3D
{

    public class AssimpToModelTests
    {

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