using UnityEngine;
using AssimpVector = Assimp.Vector3D;

namespace At.Ac.FhStp.Import3D
{
    public static class Conversion
    {
        public static Vector3 ConvertVector(AssimpVector v) => 
            new Vector3(v.X, v.Y, v.Z);
    }
}