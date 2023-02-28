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
            var color = ConvertColor(assimpMaterial.ColorDiffuse);
            
            return new MaterialModel(assimpMaterial.Name, color);
        }
        
    }
}