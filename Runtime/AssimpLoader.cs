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
                    new LibPath(Path.Combine(nativesPath, "x86")),
                    new LibPath(Path.Combine(nativesPath, "x86_64")));
            }

            LibPaths ForWindowsPlayer()
            {
                var pluginsPath = PlayerPluginsPath;
                return new LibPaths(
                    new LibPath(Path.Combine(pluginsPath, "x86")),
                    new LibPath(Path.Combine(pluginsPath, "x86_64")));
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
            library.Resolver.SetOverrideLibraryName64(paths.X64.NameOverride);
            library.Resolver.SetOverrideLibraryName32(paths.X32.NameOverride);
            library.Resolver.SetProbingPaths64(paths.X64.Path);
            library.Resolver.SetProbingPaths32(paths.X32.Path);
            return library.LoadLibrary();
        }

        private static bool ConfirmLibraryLoadedAndConfigured()
        {
            if (prevLoadAttemptSucceeded == null)
            {
                var library = AssimpLibrary.Instance;
                prevLoadAttemptSucceeded =
                    library.IsLibraryLoaded || AttemptLoad(library);
            }

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

            public LibPath X32 { get; }

            public LibPath X64 { get; }


            public LibPaths(LibPath x32, LibPath x64)
            {
                X32 = x32;
                X64 = x64;
            }

            public static LibPaths ForBoth(LibPath path) =>
                new LibPaths(path, path);

        }

        private struct LibPath
        {

            [NotNull] public string Path { get; }

            [CanBeNull] public string NameOverride { get; }


            public LibPath([NotNull] string path, [CanBeNull] string nameOverride = null)
            {
                Path = path;
                NameOverride = nameOverride;
            }

        }

    }

}