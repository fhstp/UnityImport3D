using ComradeVanti.CSharpTools;
using UnityEngine;

namespace At.Ac.FhStp.Import3D.Materials
{
    internal class MaterialModel : INamedModel
    {
        public string Name { get; }

        public Color Color { get; }

        public Color SpecularColor { get; }
        
        public IOpt<Color> EmissiveColor { get; }
        
        public float Smoothness { get; }

        public bool IsTransparent => Color.a < 1;


        public MaterialModel(string name, Color color, Color specularColor, IOpt<Color> emissiveColor, float smoothness)
        {
            Name = name;
            Color = color;
            SpecularColor = specularColor;
            EmissiveColor = emissiveColor;
            Smoothness = smoothness;
        }
    }
}