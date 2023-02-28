using System;
using System.Collections.Generic;
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
            MeshNode node,
            MeshCache meshCache, MaterialCache materialCache)
        {
            var mesh = await meshCache.Resolve(node.MeshIndex);
            var material = await materialCache.Resolve(node.MaterialIndex);

            var gameObject = await MakeGameObject(mesh.name);

            await InParallel(
                CopyMesh(mesh, gameObject),
                CopyMaterial(material, gameObject));

            return gameObject;
        }

        internal static Task<GameObject> ImportNode(
            AssimpNode node, Func<int, int> resolveMaterialIndex,
            MeshCache meshCache, MaterialCache materialCache)
        {
            async Task<GameObject> ImportFromModel(GroupNodeModel model)
            {
                var (gameObject, childNodes, meshNodes) = await InParallel(
                    MakeGameObject(model.Name),
                    InParallel(model.Children.Select(ImportFromModel)),
                    InParallel(model.MeshNodes.Select(
                        meshNode => ImportMeshNode(meshNode, meshCache, materialCache))));

                var allChildren = childNodes.Concat(meshNodes);

                await InParallel(allChildren.Select(child => CopyRelationship(gameObject, child)));

                await InParallel(
                    CopyPosition(model.Position, gameObject),
                    CopyRotation(model.Rotation, gameObject),
                    CopyScale(model.Scale, gameObject));

                return gameObject;
            }

            var model = ConvertToModel(node, resolveMaterialIndex);
            return ImportFromModel(model);
        }
    }
}