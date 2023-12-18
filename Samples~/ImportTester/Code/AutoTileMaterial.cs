#nullable enable

using UnityEngine;

namespace At.Ac.FhStp.Import3D.ImportTester
{
    [RequireComponent(typeof(Renderer))]
    [ExecuteInEditMode]
    public class AutoTileMaterial : MonoBehaviour
    {
        private static readonly int mainTexScaleKey =
            Shader.PropertyToID("_MainTex_ST");
        
        [SerializeField] private float baseMeshSize;

        private MaterialPropertyBlock propertyBlock = null!;
        private new Renderer renderer = null!;


        private void AutoTile()
        {
            var size = transform.lossyScale * baseMeshSize;
            
            propertyBlock.SetVector(mainTexScaleKey, new Vector4(size.x, size.z, 0, 0));
            renderer.SetPropertyBlock(propertyBlock);
        }

        private void Update()
        {
            AutoTile();
        }

        private void OnEnable()
        {
            renderer = GetComponent<Renderer>();
            propertyBlock = new MaterialPropertyBlock();
            AutoTile();
        }
    }
}