using UnityEngine;
using UnityEngine.InputSystem;

public class FingerController : MonoBehaviour
{
    [Header("Bones")]
    [SerializeField] private Transform baseBone;
    [SerializeField] private Transform middleBone;
    [SerializeField] private Transform tipBone;

    [Header("Input")]
    [SerializeField] private Key key;

    [Header("Curl")]
    [SerializeField] private Vector3 curlAxis = Vector3.right;

    [SerializeField] private float baseCurl = 60f;
    [SerializeField] private float middleCurl = 50f;
    [SerializeField] private float tipCurl = 40f;

    [SerializeField] private float curlSpeed = 8f;

    public float CurlAmount { get; private set; }

    Quaternion baseStart;
    Quaternion middleStart;
    Quaternion tipStart;

    void Awake()
    {
        baseStart = baseBone.localRotation;
        middleStart = middleBone.localRotation;
        tipStart = tipBone.localRotation;
    }

    void Update()
    {
        bool pressed = Keyboard.current[key].isPressed;

        float target = pressed ? 1f : 0f;

        CurlAmount = Mathf.MoveTowards(
            CurlAmount,
            target,
            curlSpeed * Time.deltaTime
        );

        ApplyCurl();
    }

    void ApplyCurl()
    {
        baseBone.localRotation =
            baseStart *
            Quaternion.AngleAxis(baseCurl * CurlAmount, curlAxis);

        middleBone.localRotation =
            middleStart *
            Quaternion.AngleAxis(middleCurl * CurlAmount, curlAxis);

        tipBone.localRotation =
            tipStart *
            Quaternion.AngleAxis(tipCurl * CurlAmount, curlAxis);
    }
}