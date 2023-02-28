using System;
using System.Threading.Tasks;
using UnityEngine;
using static At.Ac.FhStp.Import3D.TaskManagement;

namespace At.Ac.FhStp.Import3D.Materials
{
    internal static class Instantiation
    {

        private static readonly Lazy<Shader> standardShader = 
            new Lazy<Shader>(() => Shader.Find("Standard"));


        internal static Task<Material> MakeMaterial() =>
            CalcAsync(() => new Material(standardShader.Value));

    }
}