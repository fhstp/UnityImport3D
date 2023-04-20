using UnityEngine;

namespace At.Ac.FhStp.Import3D.Materials
{
    internal class MaterialModel : INamedModel
    {
        public string Name { get; }

        public Color Color { get; }

        public Color SpecularColor { get; }

        public bool IsTransparent => Color.a < 1;


        public MaterialModel(string name, Color color, Color specularColor)
        {
            Name = name;
            Color = color;
            SpecularColor = specularColor;
        }
    }
}