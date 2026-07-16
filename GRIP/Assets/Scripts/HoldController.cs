using UnityEngine;

public class HoldController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GripAnalyzer gripAnalyzer;
    [SerializeField] private Transform grabPoint;

    [Header("Finger Tips")]
    [SerializeField] private FingerTip thumb;
    [SerializeField] private FingerTip index;
    [SerializeField] private FingerTip middle;
    [SerializeField] private FingerTip ring;
    [SerializeField] private FingerTip pinky;

    [SerializeField] private float followSpeed = 15f;
    [SerializeField] private float rotateSpeed = 15f;

    private GrabbableObject heldObject;

    void Update()
    {
        if (heldObject == null)
        {
            TryGrab();
        }
        else
        {
            HoldObject();

            if (!gripAnalyzer.CanHold(heldObject))
                Release();
        }
    }

    void TryGrab()
    {
        FingerTip[] fingers =
        {
            thumb,
            index,
            middle,
            ring,
            pinky
        };

        foreach (FingerTip finger in fingers)
        {
            foreach (GrabbableObject obj in finger.TouchingObjects)
            {
                if (obj.IsHeld)
                    continue;

                if (gripAnalyzer.CanHold(obj))
                {
                    Grab(obj);
                    return;
                }
            }
        }
    }

    void Grab(GrabbableObject obj)
    {
        heldObject = obj;

        heldObject.IsHeld = true;

        heldObject.Rigidbody.isKinematic = true;

        Debug.Log("Grabbed " + obj.name);
    }

    void HoldObject()
    {
        Vector3 offset =
                heldObject.GripPoint.position -
                heldObject.transform.position;

        heldObject.transform.position =
            Vector3.MoveTowards(
                heldObject.transform.position,
                grabPoint.position - offset,
                followSpeed * Time.deltaTime);

        Quaternion rotationOffset =
            Quaternion.Inverse(heldObject.GripPoint.rotation) *
            heldObject.transform.rotation;

        heldObject.transform.rotation =
            Quaternion.Slerp(
                heldObject.transform.rotation,
                grabPoint.rotation * rotationOffset,
                rotateSpeed * Time.deltaTime);
    }

    void Release()
    {
        heldObject.IsHeld = false;

        heldObject.Rigidbody.isKinematic = false;

        heldObject = null;
    }
}