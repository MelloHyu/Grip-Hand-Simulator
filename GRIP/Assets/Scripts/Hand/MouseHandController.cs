using UnityEngine;
using UnityEngine.InputSystem;

public class MouseHandController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask tableLayer;

    [Header("Movement")]
    [SerializeField] private float followSpeed = 15f;

    [Header("Height")]
    [SerializeField] private float height = 0.35f;
    [SerializeField] private float scrollSensitivity = 0.0015f;
    [SerializeField] private float minHeight = 0.15f;
    [SerializeField] private float maxHeight = 1.2f;

    private Vector3 targetPosition;

    private void Awake()
    {
        if (cam == null)
            cam = Camera.main;

        targetPosition = transform.position;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Update()
    {
        UpdateHeight();
        UpdateTarget();
        MoveHand();
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

    private void MoveHand()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );
    }
}