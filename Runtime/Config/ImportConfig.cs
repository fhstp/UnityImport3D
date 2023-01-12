using Assimp;
using ComradeVanti.CSharpTools;
using UnityEngine;

namespace At.Ac.FhStp.Import3D
{
    /// <summary>
    ///     Allows the user to configure the import of files
    /// </summary>
    public record ImportConfig
    {
        /// <summary>
        ///     A default configuration that works for most imports
        ///     <list type="bullet">
        ///         <item>Uses no extra post-process steps</item>
        ///         <item>Uses the file-name for the scene-name</item>
        ///         <item>Instantiated scene is not attached to any game-object</item>
        ///     </list>
        /// </summary>
        public static readonly ImportConfig Default =
            new ImportConfig
            {
                ExtraAssimpPostProcessSteps = PostProcessSteps.None,
                SceneNameOverride = Opt.None<string>(),
                Parent = Opt.None<Transform>()
            };

        /// <summary>
        ///     Extra post-process-steps that assimp applies during import.
        ///     Triangulate is enforced for every import by default
        /// </summary>
        /// <seealso href="https://documentation.help/assimp/postprocess_8h.html#a64795260b95f5a4b3f3dc1be4f52e410">Assimp documentation</seealso>
        public PostProcessSteps ExtraAssimpPostProcessSteps
        {
            get;
            private init;
        }

        /// <summary>
        ///     By default the name of the scene is taken to be name name of the imported file.
        ///     If this property is set so Some string it will override this behaviour.
        /// </summary>
        public IOpt<string> SceneNameOverride { get; private init; }

        /// <summary>
        ///     A transform to parent the instantiated scene to
        /// </summary>
        public IOpt<Transform> Parent { get; private init; }


        /// <summary>
        ///     Replaces the extra post-processing steps in this config
        /// </summary>
        /// <param name="steps">The replacement steps</param>
        /// <returns>Updated config</returns>
        public ImportConfig WithExtraAssimpPostProcessSteps(
            PostProcessSteps steps) =>
            this with {ExtraAssimpPostProcessSteps = steps};

        /// <summary>
        ///     By default the scene-name is the name of the file the scene was
        ///     loaded from. Use this function to specify a custom name
        /// </summary>
        /// <param name="name">The name</param>
        /// <returns>The updated config</returns>
        public ImportConfig WithSceneName(string name) => this with
        {
            SceneNameOverride = Opt.Some(name)
        };

        /// <summary>
        ///     Sets the transform that instantiated scenes will be attached to
        /// </summary>
        /// <param name="transform">The transform</param>
        /// <returns>The updated config</returns>
        public ImportConfig WithParent(Transform transform) => this with
        {
            Parent = Opt.Some(transform)
        };
    }
}