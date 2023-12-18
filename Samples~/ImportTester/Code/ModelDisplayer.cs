#nullable enable

using UnityEngine;
using UnityEngine.Events;

namespace At.Ac.FhStp.Import3D.ImportTester
{
    public class ModelDisplayer : MonoBehaviour
    {
        [SerializeField] private Transform modelParent = null!;

        [SerializeField] private UnityEvent<Transform?> displayedModelChanged =
            new UnityEvent<Transform?>();

        private int modelCount;
        private int? modelIndex;


        private void UpdateDisplayedModel()
        {
            if (modelIndex == null) return;

            var current = modelParent.GetChild(modelIndex.Value);
            current.gameObject.SetActive(true);

            displayedModelChanged.Invoke(current);
        }

        public void OnModelsLoaded()
        {
            modelCount = modelParent.childCount;
            modelIndex = modelCount > 0 ? 0 : null;

            if (modelIndex != null)
                UpdateDisplayedModel();
            else
                displayedModelChanged.Invoke(null);
        }

        public void OnModelSwitchInput(LinearDirection direction)
        {
            if (modelIndex == null) return;

            var prev = modelParent.GetChild(modelIndex.Value);
            prev.gameObject.SetActive(false);

            modelIndex = (int) Mathf.Repeat(modelIndex.Value + (int) direction, modelCount);
            UpdateDisplayedModel();
        }
    }
}