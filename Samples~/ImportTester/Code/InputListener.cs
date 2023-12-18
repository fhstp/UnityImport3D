using UnityEngine;
using UnityEngine.Events;

namespace At.Ac.FhStp.Import3D.ImportTester
{
    public class InputListener : MonoBehaviour
    {
        [SerializeField] private UnityEvent<LinearDirection> modelSwitchInput =
                new UnityEvent<LinearDirection>();
        [SerializeField] private UnityEvent reloadModelsInput =
                new UnityEvent();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                modelSwitchInput.Invoke(LinearDirection.Previous);
            else if (Input.GetKeyDown(KeyCode.E))
                modelSwitchInput.Invoke(LinearDirection.Next);
            else if(Input.GetKeyDown(KeyCode.R))
                reloadModelsInput.Invoke();
        }
    }
}