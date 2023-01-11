using Assimp;

namespace At.Ac.FhStp.Import3D
{
    /// <summary>
    ///     Allows the user to configure the import of files
    /// </summary>
    public record ImportConfig
    {
        /// <summary>
        ///     A default configuration that works for most imports
        /// </summary>
        public static readonly ImportConfig Default =
            new ImportConfig
            {
                AssimpPostProcessSteps = PostProcessSteps.Triangulate
            };

        /// <summary>
        ///     Post-process-steps that assimp applies during import
        /// </summary>
        /// <seealso href="https://documentation.help/assimp/postprocess_8h.html#a64795260b95f5a4b3f3dc1be4f52e410">Assimp documentation</seealso>
        public PostProcessSteps AssimpPostProcessSteps { get; init; }
    }
}