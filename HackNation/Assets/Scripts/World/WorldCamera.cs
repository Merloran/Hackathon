using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using System.Collections.Generic;

public class WorldCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Settings")]
    public float distance = 10f;
    public float zoomRate = 0.05f;
    public Vector2 rotationSpeed = new Vector2(0.05f, 0.05f);
    public Vector2 yLimits = new Vector2(-90f, 90f);
    public Vector2 zoomLimits = new Vector2(10f, 30f);
    public float dragThreshold = 10f;

    [SerializeField] private LayerMask clickLayer;

    private float x = 0.0f;
    private float y = 0.0f;
    private Vector2 totalDragDelta = Vector2.zero;
    private bool isDragging = false;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        if (target != null) UpdateCameraPosition();
    }

    void LateUpdate()
    {
        if (!target) return;
        if (Touchscreen.current == null) return;

        var pressedTouches = new List<TouchControl>();
        TouchControl releasedTouch = null;

        foreach (var touch in Touchscreen.current.touches)
        {
            if (touch.press.isPressed)
            {
                pressedTouches.Add(touch);
            }
            else if (touch.press.wasReleasedThisFrame)
            {
                releasedTouch = touch;
            }
        }

        int touchCount = pressedTouches.Count;

        if (touchCount == 2) // ZOOM
        {
            isDragging = true;

            var t0 = pressedTouches[0];
            var t1 = pressedTouches[1];

            Vector2 t0Pos = t0.position.ReadValue();
            Vector2 t1Pos = t1.position.ReadValue();
            Vector2 t0Prev = t0Pos - t0.delta.ReadValue();
            Vector2 t1Prev = t1Pos - t1.delta.ReadValue();

            float prevMag = (t0Prev - t1Prev).magnitude;
            float currentMag = (t0Pos - t1Pos).magnitude;

            distance += (prevMag - currentMag) * zoomRate;
        }
        else if (touchCount == 1) // DRAG
        {
            var t0 = pressedTouches[0];

            if (t0.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began)
            {
                isDragging = false;
                totalDragDelta = Vector2.zero;
            }

            if (t0.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Moved)
            {
                Vector2 delta = t0.delta.ReadValue();
                totalDragDelta += delta;

                x += delta.x * rotationSpeed.x;
                y -= delta.y * rotationSpeed.y;

                if (totalDragDelta.magnitude > dragThreshold)
                {
                    isDragging = true;
                }
            }
        }
        else if (touchCount == 0 && releasedTouch != null) // TAP
        {
            if (!isDragging)
            {
                DoClick(releasedTouch.position.ReadValue());
            }

            isDragging = false;
            totalDragDelta = Vector2.zero;
        }

        y = ClampAngle(y, yLimits.x, yLimits.y);
        distance = Mathf.Clamp(distance, zoomLimits.x, zoomLimits.y);
        UpdateCameraPosition();
    }

    void DoClick(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, clickLayer))
        {
            var interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.OnClick();
            }
            else
            {
                Debug.Log("Trafiono: " + hit.collider.name);
            }
        }
    }

    void UpdateCameraPosition()
    {
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
        transform.rotation = rotation;
        transform.position = position;
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F) angle += 360F;
        if (angle > 360F) angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}