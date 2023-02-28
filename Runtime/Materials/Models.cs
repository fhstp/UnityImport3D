namespace At.Ac.FhStp.Import3D.Materials
{
    internal class MaterialModel : INamedModel
    {
        public string Name { get; }

        
        public MaterialModel(string name) => 
            Name = name;
    }
}