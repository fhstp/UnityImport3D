#nullable enable

using System;
using System.Linq;
using UnityEngine;

namespace At.Ac.FhStp.Import3D.ImportTester
{
    public class FloorFitter : MonoBehaviour
    {
        [SerializeField] private float floorMeshSize;

        private new Transform transform = null!;

        private void Awake()
        {
            transform = base.transform;
        }

        public void Fit(Transform? model)
        {
            var renderers =
                model
                    ? model!.GetComponentsInChildren<MeshRenderer>()
                    : Array.Empty<MeshRenderer>();

            var meshBounds = renderers.Select(it => it.bounds);

            var bound = meshBounds.Aggregate(new Bounds(), (bounds, meshBound) =>
            {
                bounds.Encapsulate(meshBound);
                return bounds;
            });


            var scale = (bound.size + new Vector3(1, 0, 1)) / floorMeshSize;
            transform.localScale = new Vector3(scale.x, 1, scale.z);
            transform.position = bound.center - new Vector3(0, bound.size.y / 2f + 0.05f, 0);
        }
    }
}