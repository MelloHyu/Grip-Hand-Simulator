using UnityEngine;
using UnityEngine.InputSystem;

public class MouseHandController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask tableLayer;

    [Header("Movement")]
    [SerializeField] private float followSpeed = 15f;

    [Header("Rotation")]
    [SerializeField] private Transform handPivot;
    [SerializeField] private float rotationSpeed = 0.15f;
    [SerializeField] private float maxPitch = 70f;

    [Header("Height")]
    [SerializeField] private float height = 0.35f;
    [SerializeField] private float scrollSensitivity = 0.0015f;
    [SerializeField] private float minHeight = 0.15f;
    [SerializeField] private float maxHeight = 1.2f;

    private Vector3 targetPosition;

    private float yaw;
    private float pitch;
    private bool wasRotating = false;
    private Vector2 mousePositionBeforeRotation;

    private void Awake()
    {
        if (cam == null)
            cam = Camera.main;

        targetPosition = transform.position;

        if (handPivot != null)
        {
            Vector3 rot = handPivot.localEulerAngles;

            pitch = rot.x;
            yaw = rot.y;
        }

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Update()
    {
        UpdateHeight();

        bool isRotating = Mouse.current.middleButton.isPressed;

        if (isRotating)
        {
            if (!wasRotating)
            {
                StartRotation();
            }

            RotateHand();
        }
        else
        {
            if (wasRotating)
            {
                EndRotation();
            }

            UpdateTarget();
            MoveHand();
        }

        wasRotating = isRotating;
    }

    private void UpdateHeight()
    {
        if (Mouse.current == null)
            return;

        float scroll = Mouse.current.scroll.ReadValue().y;

        height += scroll * scrollSensitivity;

        height = Mathf.Clamp(height, minHeight, maxHeight);
    }

    private void UpdateTarget()
    {
        if (Mouse.current == null)
            return;

        Vector2 mousePos = Mouse.current.position.ReadValue();

        Ray ray = cam.ScreenPointToRay(mousePos);

        Plane tablePlane = new Plane(Vector3.up, Vector3.zero);

        if (tablePlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);

            targetPosition = hitPoint;
            targetPosition.y = height;
        }
    }
    private void StartRotation()
    {
        mousePositionBeforeRotation = Mouse.current.position.ReadValue();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void EndRotation()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        Mouse.current.WarpCursorPosition(mousePositionBeforeRotation);
    }
    private void RotateHand()
    {
        if (Mouse.current == null)
            return;

        Vector2 delta = Mouse.current.delta.ReadValue();

        // Rotate around WORLD Y
        handPivot.Rotate(Vector3.up,
            delta.x * rotationSpeed,
            Space.World);

        // Rotate around LOCAL X (or Z if that's your preferred wrist tilt)
        handPivot.Rotate(Vector3.forward,
            -delta.y * rotationSpeed,
            Space.Self);
    }
    private void MoveHand()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );
    }
}