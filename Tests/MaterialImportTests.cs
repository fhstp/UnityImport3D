using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;
using static At.Ac.FhStp.Import3D.Materials.Import;

namespace At.Ac.FhStp.Import3D
{
    [RequiresPlayMode]
    public class MaterialImportTests
    {
        
        [Test]
        public async Task Material_Name_Is_Assimp_Name()
        {
            var assimpMaterial = new Assimp.Material
            {
                Name = "MyMaterial"
            };

            var material = await ImportMaterial(assimpMaterial);
            
            Assert.AreEqual(assimpMaterial.Name, material.name);
        }
        
    }
}