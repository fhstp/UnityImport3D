namespace At.Ac.FhStp.Import3D
{
    public record MeshImportConfig(float ScalingFactor)
    {
        public static readonly MeshImportConfig Default = new MeshImportConfig(1);
    };
}