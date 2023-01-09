using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using Assimp;
using Assimp.Unmanaged;
using JetBrains.Annotations;
using UnityEngine;
using AssimpScene = Assimp.Scene;
using static At.Ac.FhStp.Import3D.TaskManagement;

namespace At.Ac.FhStp.Import3D
{
    internal static class AssimpLoader
    {
        [SuppressMessage("ReSharper", "BitwiseOperatorOnEnumWithoutFlags")]
        private const RuntimePlatform EditorPlatforms =
            RuntimePlatform.LinuxEditor
            | RuntimePlatform.WindowsEditor
            | RuntimePlatform.OSXEditor;

        private static bool? prevLoadAttemptSucceeded;


        private static bool PlatformIsEditor =>
            EditorPlatforms.HasFlag(Application.platform);

        private static string EditorNativesPath
        {
            get
            {
                var packagePath = Path.GetFullPath("Packages/at.ac.fhstp.import3d");
                var pluginsPath = Path.Combine(packagePath, "Plugins");
                return Path.Combine(pluginsPath, "Assimp", "Native");
            }
        }

        private static string PlayerDataPath =>
            Application.dataPath;


        private static string PlayerPluginsPath =>
            Path.Combine(PlayerDataPath, "Plugins");


        private static LibPaths? TryFindPaths()
        {
            LibPaths ForWindowsEditor()
            {
                var nativesPath = Path.Combine(EditorNativesPath, "win");
                return new LibPaths(
                    Path.Combine(nativesPath, "x86", "assimp.dll"),
                    Path.Combine(nativesPath, "x86_64", "assimp.dll"));
            }

            LibPaths ForWindowsPlayer()
            {
                var pluginsPath = PlayerPluginsPath;
                return new LibPaths(
                    Path.Combine(pluginsPath, "x86", "assimp.dll"),
                    Path.Combine(pluginsPath, "x86_64", "assimp.dll"));
            }


            return Application.platform switch
            {
                RuntimePlatform.WindowsEditor => ForWindowsEditor(),
                RuntimePlatform.WindowsPlayer => ForWindowsPlayer(),
                _ => null
            };
        }

        private static bool AttemptLoad(UnmanagedLibrary library)
        {
            var maybePaths = TryFindPaths();

            if (maybePaths == null)
            {
                Debug.LogError("Import3D does not support " + Application.platform);
                return false;
            }

            var paths = maybePaths.Value;
            return library.LoadLibrary(paths.X32, paths.X64);
        }

        private static bool ConfirmLibraryLoadedAndConfigured()
        {
            if (prevLoadAttemptSucceeded != null) return prevLoadAttemptSucceeded.Value;

            var library = AssimpLibrary.Instance;
            prevLoadAttemptSucceeded =
                library.IsLibraryLoaded || AttemptLoad(library);

            return prevLoadAttemptSucceeded.Value;
        }

        private static AssimpContext MakeContext()
        {
            ConfirmLibraryLoadedAndConfigured();
            return new AssimpContext();
        }

        internal static async Task<AssimpScene> LoadSceneFrom(string path)
        {
            var ctx = MakeContext();
            var scene = await InBackground(
                () => ctx.ImportFile(path, PostProcessSteps.Triangulate));
            ctx.Dispose();
            return scene;
        }

        private struct LibPaths
        {
            public string X32 { get; }

            public string X64 { get; }


            public LibPaths(string x32, string x64)
            {
                X32 = x32;
                X64 = x64;
            }

            public static LibPaths ForBoth(string path)
            {
                return new LibPaths(path, path);
            }
        }
    }
}