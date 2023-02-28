namespace At.Ac.FhStp.Import3D.Materials
{
    internal static class ModelConversion
    {

        internal static MaterialModel ConvertToModel(Assimp.Material assimpMaterial)
        {
            return new MaterialModel(assimpMaterial.Name);
        }
        
    }
}