using UnityEngine;

namespace At.Ac.FhStp.Import3D.ImportTester
{
    public class ModelDisplayer : MonoBehaviour
    {
        [SerializeField] private Transform modelParent = null!;

        private int modelCount;
        private int? modelIndex;


        private void UpdateDisplayedModel()
        {
            if (modelIndex == null) return;

            var current = modelParent.GetChild(modelIndex.Value);
            current.gameObject.SetActive(true);
        }

        public void OnModelsLoaded()
        {
            modelCount = modelParent.childCount;
            modelIndex = modelCount > 0 ? 0 : null;

            UpdateDisplayedModel();
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