namespace At.Ac.FhStp.Import3D
{
    public record GroupNodeImportConfig(
        float ScalingFactor,
        bool Hidden)
    {
        public static readonly GroupNodeImportConfig Default = new GroupNodeImportConfig(1, true);
    };
}