using System.Collections.Generic;
using UnityEngine;
public enum FingerType
{
    Thumb,
    Index,
    Middle,
    Ring,
    Pinky
}
public class FingerTip : MonoBehaviour
{
    [SerializeField]
    private FingerType finger;

    private readonly HashSet<GrabbableObject> touchingObjects = new();

    public IReadOnlyCollection<GrabbableObject> TouchingObjects => touchingObjects;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out GrabbableObject obj))
        {
            touchingObjects.Add(obj);
            obj.AddFinger(finger);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out GrabbableObject obj))
        {
            touchingObjects.Remove(obj);
            obj.RemoveFinger(finger);
        }
    }
}