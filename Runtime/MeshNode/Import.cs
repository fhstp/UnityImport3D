using System.Threading.Tasks;
using UnityEngine;
using AssimpMesh = Assimp.Mesh;
using static At.Ac.FhStp.Import3D.Common.Instantiation;
using static At.Ac.FhStp.Import3D.TaskManagement;
using static At.Ac.FhStp.Import3D.MeshNode.DataCopy;

namespace At.Ac.FhStp.Import3D.MeshNode
{

    internal static class Import
    {

        internal static async Task<GameObject> ImportMeshNode(
            int meshIndex, MeshCache meshCache)
        {
            var mesh = await meshCache.Resolve(meshIndex);
            var material = new Material(Shader.Find("Standard"));
            
            var gameObject = await MakeGameObject(mesh.name);

            await InParallel(
                CopyMesh(mesh, gameObject),
                CopyMaterial(material, gameObject));
            
            return gameObject;
        }

    }

}