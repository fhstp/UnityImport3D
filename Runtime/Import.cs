using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using static At.Ac.FhStp.Import3D.Scenes.Import;
using static At.Ac.FhStp.Import3D.Meshes.Import;
using static At.Ac.FhStp.Import3D.TaskManagement;

namespace At.Ac.FhStp.Import3D
{
    /// <summary>
    ///     Contains functions for importing 3D objects
    /// </summary>
    public static class Import
    {
        /// <summary>
        ///     Imports a 3D-model file from a given path
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <param name="config">Configuration for this import</param>
        /// <returns>A task, producing the imported scene</returns>
        public static async Task<GameObject> SingleAsync(string path, [CanBeNull] ImportConfig config = null)
        {
            config ??= ImportConfig.Default;
            var assimpScene = await AssimpLoader.LoadSceneFrom(path,
                config.ExtraAssimpPostProcessSteps);
            return await ImportScene(assimpScene, path, config);
        }

        /// <summary>
        ///     Imports all meshes from 3D-model file at a given path
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <param name="config">Configuration for this import</param>
        /// <returns>A task, producing the meshes</returns>
        public static async Task<IEnumerable<Mesh>> MeshesFromSingleAsync(string path, [CanBeNull] ImportConfig config = null)
        {
            config ??= ImportConfig.Default;
            var assimpScene = await AssimpLoader.LoadSceneFrom(path,
                config.ExtraAssimpPostProcessSteps);
            var meshImportConfig = new MeshImportConfig(
                assimpScene.GetScalingFactor());

            return await InParallel(
                assimpScene.Meshes.Select(mesh => ImportMesh(mesh, meshImportConfig)));
        }

        /// <summary>
        /// Checks if a file is supported for import
        /// </summary>
        /// <param name="path">The file of the path</param>
        /// <returns>Whether the file can be imported</returns>
        public static bool SupportsFileExtension(string path)
        {
            var extension = Path.GetExtension(path);
            /*
             NOTE: Every time this function is called, an
             AssimpContext is created and I don't know if that is bad.
             */
            return AssimpLoader.MakeContext().IsImportFormatSupported(extension);
        }
    }
}