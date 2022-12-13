using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using AssimpScene = Assimp.Scene;
using static At.Ac.FhStp.Import3D.Meshes.Import;
using static At.Ac.FhStp.Import3D.Nodes.Import;

namespace At.Ac.FhStp.Import3D.Scenes
{

    internal static class Import
    {

        internal static async Task<GameObject> ImportScene(AssimpScene scene, string filePath)
        {
            var sceneName = Path.GetFileNameWithoutExtension(filePath);
            
            Task<Mesh> ImportMeshWithIndex(int index) =>
                ImportMesh(scene.Meshes[index]);
            
            var meshCache = new MeshCache(ImportMeshWithIndex);

            var root = await ImportNode(scene.RootNode, meshCache);
            root.name = sceneName;
            return root;
        }

    }

}