using UnityEngine;

namespace At.Ac.FhStp.Import3D.Materials
{
    internal static class ModelConversion
    {
        private static Color ConvertColor(Assimp.Color4D color)
        {
            return new Color(color.R, color.G, color.B, color.A);
        }

        internal static MaterialModel ConvertToModel(Assimp.Material assimpMaterial)
        {
            var diffuse = assimpMaterial.HasColorDiffuse
                ? ConvertColor(assimpMaterial.ColorDiffuse)
                : Color.black;
            var ambient = assimpMaterial.HasColorAmbient
                ? ConvertColor(assimpMaterial.ColorAmbient)
                : Color.black;
            var color = diffuse + ambient;
            color.a *= assimpMaterial.Opacity;

            return new MaterialModel(assimpMaterial.Name, color);
        }
    }
}