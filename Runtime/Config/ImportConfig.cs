using Assimp;
using ComradeVanti.CSharpTools;

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
        ///     </list>
        /// </summary>
        public static readonly ImportConfig Default =
            new ImportConfig
            {
                AssimpPostProcessSteps = PostProcessSteps.Triangulate,
                SceneNameOverride = Opt.None<string>()
            };

        /// <summary>
        ///     Post-process-steps that assimp applies during import
        /// </summary>
        /// <seealso href="https://documentation.help/assimp/postprocess_8h.html#a64795260b95f5a4b3f3dc1be4f52e410">Assimp documentation</seealso>
        public PostProcessSteps AssimpPostProcessSteps { get; init; }

        /// <summary>
        ///     By default the name of the scene is taken to be name name of the imported file.
        ///     If this property is set so Some string it will override this behaviour.
        /// </summary>
        public IOpt<string> SceneNameOverride { get; init; }
    }
}