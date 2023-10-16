namespace At.Ac.FhStp.Import3D
{
    public record GroupNodeImportConfig(float ScalingFactor)
    {
        public static readonly GroupNodeImportConfig Default = new GroupNodeImportConfig(1);
    };
}