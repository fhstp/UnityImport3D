using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using AssimpMesh = Assimp.Mesh;
using AssimpNode = Assimp.Node;
using static At.Ac.FhStp.Import3D.Common.Instantiation;
using static At.Ac.FhStp.Import3D.TaskManagement;
using static At.Ac.FhStp.Import3D.Nodes.DataCopy;
using static At.Ac.FhStp.Import3D.Nodes.ModelConversion;

namespace At.Ac.FhStp.Import3D.Nodes
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

        internal static Task<GameObject> ImportNode(
            AssimpNode node, MeshCache meshCache)
        {
            async Task<GameObject> ImportFromModel(GroupNodeModel model)
            {
                var (gameObject, childNodes, meshNodes) = await InParallel(
                    MakeGameObject(model.Name),
                    InParallel(model.Children.Select(ImportFromModel)),
                    InParallel(model.MeshIndices.Select(i => ImportMeshNode(i, meshCache))));

                await InParallel(childNodes.Concat(meshNodes)
                    .Select(child => CopyRelationship(gameObject, child)));

                return gameObject;
            }

            var model = ConvertToModel(node);
            return ImportFromModel(model);
        }
    }
}