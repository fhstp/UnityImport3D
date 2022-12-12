using System.Threading.Tasks;
using NUnit.Framework;
using AssimpTexel = Assimp.Texel;
using static At.Ac.FhStp.Import3D.AssimpTextureBuilding;
using static At.Ac.FhStp.Import3D.Textures.Import;

namespace At.Ac.FhStp.Import3D
{

    public class NonCompressedTextureImportTests
    {

        [Test]
        public async Task Texture_Name_Is_Assimp_Name_Without_Extension()
        {
            var assimpTexture = MakeSolidBlackTexture("image.png", 1, 1);

            var texture = await ImportTexture(assimpTexture);

            Assert.AreEqual("image", texture.name);
        }

        [Test]
        public async Task Texture_Size_Is_Assimp_Size()
        {
            const int width = 2;
            const int height = 4;
            var assimpTexture = MakeSolidBlackTexture("image.png", width, height);

            var texture = await ImportTexture(assimpTexture);

            Assert.AreEqual(width, texture.width, "Width");
            Assert.AreEqual(height, texture.height, "Height");
        }

        [Test]
        public async Task Texture_Pixels_Are_Normalized_Assimp_Texels()
        {
            var texel = new AssimpTexel(10, 20, 30, 40);
            var assimpTexture = MakeSolidColorTexture("image.png", 1, 1, texel);

            var texture = await ImportTexture(assimpTexture);
            var pixel = texture.GetPixel(0, 0);

            Assert.AreEqual(30 / 255f, pixel.r, "R");
            Assert.AreEqual(20 / 255f, pixel.g, "G");
            Assert.AreEqual(10 / 255f, pixel.b, "B");
            Assert.AreEqual(40 / 255f, pixel.a, "A");
        }

    }

}