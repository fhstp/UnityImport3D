using ComradeVanti.CSharpTools;
using UnityEngine;

namespace At.Ac.FhStp.Import3D.Materials
{
    internal static class ModelConversion
    {
        private static Color ConvertColor(Assimp.Color4D color)
        {
            return new Color(color.R, color.G, color.B, color.A);
        }

        private static Color MultRGB(this Color c, float f) =>
            new Color(c.r * f, c.g * f, c.b * f, c.a);

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

            var specular = assimpMaterial.HasColorSpecular
                ? ConvertColor(assimpMaterial.ColorSpecular)
                : Color.black;
            specular = specular.MultRGB(assimpMaterial.ShininessStrength);

            var emissive = assimpMaterial.HasColorEmissive
                ? Opt.Some(ConvertColor(assimpMaterial.ColorEmissive))
                : Opt.None<Color>();

            /* I dont know if reflectivity is actually the right property
            *  to use here. There is pretty much no documentation about it,
            *  but at least it is [0,1] or at least seems that way. */
            var smoothness = assimpMaterial.HasReflectivity
                ? assimpMaterial.Reflectivity
                : 0.5f;

            return new MaterialModel(
                assimpMaterial.Name, color, specular, emissive, smoothness);
        }
    }
}