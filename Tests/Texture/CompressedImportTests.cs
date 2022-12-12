using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using static At.Ac.FhStp.Import3D.Texture.AssimpTextureBuilding;
using static At.Ac.FhStp.Import3D.Texture.Import;

namespace At.Ac.FhStp.Import3D.Texture
{

    [RequiresPlayMode]
    public class CompressedImportTests
    {

        [Test]
        public async Task Texture_Name_Is_Assimp_Name()
        {
            const string name = "image";
            var assimpTexture = Make1x1Png(name, Color.black);

            var texture = await ImportTexture(assimpTexture);

            Assert.AreEqual(name, texture.name);
        }

        [Test]
        public async Task Texture_Size_Comes_From_Assimp_Bytes()
        {
            const int width = 2;
            const int height = 4;
            var assimpTexture = MakeColorPng("image", width, height, Color.black);

            var texture = await ImportTexture(assimpTexture);

            Assert.AreEqual(width, texture.width, "Width");
            Assert.AreEqual(height, texture.height, "Height");
        }

        [Test]
        public async Task Texture_Pixels_Come_From_Assimp_Bytes()
        {
            const int width = 1;
            const int height = 1;
            var assimpTexture = MakeColorPng("image", width, height, Color.green);

            var texture = await ImportTexture(assimpTexture);

            Assert.AreEqual(Color.green, texture.GetPixel(0, 0));
        }

    }

}