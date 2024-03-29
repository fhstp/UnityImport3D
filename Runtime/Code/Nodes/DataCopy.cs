using System.Threading.Tasks;
using Dev.ComradeVanti;
using UnityEngine;
using static At.Ac.FhStp.Import3D.TaskManagement;

namespace At.Ac.FhStp.Import3D.Nodes
{
    internal static class DataCopy
    {
        internal static Task<Nothing> CopyMesh(
            Mesh mesh, GameObject gameObject) =>
            DoAsync(() =>
            {
                var meshFilter = gameObject.AddComponent<MeshFilter>();
                meshFilter.mesh = mesh;
            });

        internal static Task<Nothing> CopyMaterial(
            Material material, GameObject gameObject) =>
            DoAsync(() =>
            {
                var meshRenderer = gameObject.AddComponent<MeshRenderer>();
                meshRenderer.material = material;
            });

        internal static Task<Nothing> CopyRelationship(
            GameObject parent, GameObject child) =>
            DoAsync(() => child.transform.SetParent(parent.transform, false));

        internal static Task<Nothing> CopyPosition(
            Vector3 position, GameObject gameObject) =>
            DoAsync(() => gameObject.transform.localPosition = position);
        
        internal static Task<Nothing> CopyRotation(
            Quaternion quaternion, GameObject gameObject) =>
            DoAsync(() => gameObject.transform.localRotation = quaternion);
        
        internal static Task<Nothing> CopyScale(
            Vector3 scale, GameObject gameObject) =>
            DoAsync(() => gameObject.transform.localScale = scale);

        internal static Task<Nothing> CopyIsActive(
            bool isActive, GameObject gameObject) =>
            DoAsync(() => gameObject.SetActive(isActive));
    }
}