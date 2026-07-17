using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    Apple,
    Coin,
    Key,
    Mug
}

[RequireComponent(typeof(Rigidbody))]
public class GrabbableObject : MonoBehaviour
{
    [SerializeField]
    private GripProfile gripProfile;

    public GripProfile GripProfile => gripProfile;

    public Rigidbody Rigidbody { get; private set; }

    public bool IsHeld { get; set; }

    public ConfigurableJoint HoldJoint { get; set; }

    private readonly HashSet<FingerType> touchingFingers = new();

    public IReadOnlyCollection<FingerType> TouchingFingers => touchingFingers;

    [SerializeField] private Transform gripPoint;

    public Transform GripPoint => gripPoint;

    [Header("Object")]

    [SerializeField] private ObjectType objectType;

    public ObjectType ObjectType => objectType;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void AddFinger(FingerType finger)
    {
        touchingFingers.Add(finger);
    }

    public void RemoveFinger(FingerType finger)
    {
        touchingFingers.Remove(finger);
    }

    public bool IsTouching(FingerType finger)
    {
        return touchingFingers.Contains(finger);
    }

    public void ClearContacts()
    {
        touchingFingers.Clear();
    }
}