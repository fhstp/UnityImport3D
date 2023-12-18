#nullable enable

using System;
using System.Linq;
using UnityEngine;

namespace At.Ac.FhStp.Import3D.ImportTester
{
    public class FloorFitter : MonoBehaviour
    {
        [SerializeField] private float floorMeshSize;

        public void Fit(Transform? model)
        {
            var meshFilters =
                (model
                    ? model!.GetComponentsInChildren<MeshFilter>()
                    : Array.Empty<MeshFilter>());

            var meshBounds = meshFilters.Select(meshFilter =>
            {
                var meshBounds = meshFilter.mesh.bounds;
                var meshTransform = meshFilter.transform;
                var worldSpaceMeshBounds = new Bounds(
                    meshTransform.TransformPoint(meshBounds.center),
                    meshTransform.TransformDirection(meshBounds.size));
                return worldSpaceMeshBounds;
            });

            var bound = meshBounds.Aggregate(new Bounds(), (bounds, meshBound) =>
            {
                bounds.Encapsulate(meshBound);
                return bounds;
            });

            transform.localScale = (bound.size + new Vector3(1, 0, 1)) / floorMeshSize;
            transform.position = bound.center - new Vector3(0, bound.size.y / 2f + 0.05f, 0);
        }
    }
}