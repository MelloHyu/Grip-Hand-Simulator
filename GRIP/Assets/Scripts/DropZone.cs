using UnityEngine;

public class DropZone : MonoBehaviour
{
    [SerializeField] private SimonGameManager gameManager;

    private GrabbableObject currentObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out GrabbableObject obj))
        {
            currentObject = obj;
            gameManager.BeginAssessment(obj);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out GrabbableObject obj))
        {
            if (obj == currentObject)
            {
                currentObject = null;
                gameManager.CancelAssessment();
            }
        }
    }
}