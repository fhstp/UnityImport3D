using System.Threading.Tasks;
using UnityEngine;
using AssimpScene = Assimp.Scene;
using static At.Ac.FhStp.Import3D.Meshes.Import;
using static At.Ac.FhStp.Import3D.Nodes.Import;

namespace At.Ac.FhStp.Import3D.Scenes
{

    internal static class Import
    {

        internal static Task<GameObject> ImportScene(AssimpScene scene)
        {
            Task<Mesh> ImportMeshWithIndex(int index) =>
                ImportMesh(scene.Meshes[index]);
            
            var meshCache = new MeshCache(ImportMeshWithIndex);

            return ImportNode(scene.RootNode, meshCache);
        }

    }

}