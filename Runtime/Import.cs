using System.Threading.Tasks;
using Assimp;
using UnityEngine;
using static At.Ac.FhStp.Import3D.TaskManagement;
using static At.Ac.FhStp.Import3D.Scenes.Import;
using AssimpScene = Assimp.Scene;

namespace At.Ac.FhStp.Import3D
{

    /// <summary>
    ///     Contains functions for importing 3D objects
    /// </summary>
    public static class Import
    {

        private static AssimpContext MakeContext() =>
            new AssimpContext();

        private static async Task<AssimpScene> ImportSceneFile(string path)
        {
            var ctx = MakeContext();
            var scene = await InBackground(() => ctx.ImportFile(path, PostProcessSteps.Triangulate));
            ctx.Dispose();
            return scene;
        }

        /// <summary>
        ///     Imports a 3D-model file from a given path
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <returns>A task, producing the imported scene</returns>
        public static async Task<GameObject> SingleAsync(string path)
        {
            var assimpScene = await ImportSceneFile(path);
            return await ImportScene(assimpScene, path);
        }

    }

}