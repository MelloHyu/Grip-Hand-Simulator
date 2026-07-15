using UnityEngine;
using static GestureRecognizer;

[CreateAssetMenu(fileName = "GripProfile", menuName = "GRIP/Grip Profile")]
public class GripProfile : ScriptableObject
{
    [Header("Grip")]
    public GripType requiredGrip;

    [Header("Finger Contributions")]
    public FingerRequirement[] fingerRequirements;

    [Header("Stability")]

    [Range(0, 100)]
    public int stabilityRequired = 75;

    [Header("Hold")]

    public float grabDistance = 0.06f;

    public float holdForce = 1000f;

    public float breakForce = 100f;

    [Header("Physics Hold")]

    public float followForce = 250f;

    public float damping = 18f;

    public float rotationForce = 30f;

    public float maxVelocity = 5f;
}