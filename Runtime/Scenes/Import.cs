using System.IO;
using System.Threading.Tasks;
using ComradeVanti.CSharpTools;
using UnityEngine;
using static At.Ac.FhStp.Import3D.Materials.Import;
using AssimpScene = Assimp.Scene;
using static At.Ac.FhStp.Import3D.Meshes.Import;
using static At.Ac.FhStp.Import3D.Nodes.Import;

namespace At.Ac.FhStp.Import3D.Scenes
{
    internal static class Import
    {
        private static string MakeSceneNameFrom(string filePath) =>
            Path.GetFileNameWithoutExtension(filePath);

        internal static async Task<GameObject> ImportScene(
            AssimpScene scene,
            string filePath, ImportConfig config)
        {
            var sceneName = config.SceneNameOverride
                .DefaultWith(() => MakeSceneNameFrom(filePath));

            Task<Mesh> ImportMeshWithIndex(int index) =>
                ImportMesh(scene.Meshes[index]);
            
            
            Task<Material> ImportMaterialWithIndex(int index) =>
                ImportMaterial(scene.Materials[index]);

            var meshCache = new MeshCache(ImportMeshWithIndex);
            var materialCache = new MaterialCache(ImportMaterialWithIndex);

            int ResolveMaterialIndex(int meshIndex) => 
                scene.Meshes[meshIndex].MaterialIndex;

            var root = await ImportNode(
                scene.RootNode, ResolveMaterialIndex,
                meshCache, materialCache);
            root.name = sceneName;
            config.Parent.Iter(it => root.transform.SetParent(it, false));
            return root;
        }
    }
}