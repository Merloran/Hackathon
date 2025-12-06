using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using System.Collections.Generic;


public class WorldCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Settings")]
    public float distance = 10.0f;
    public float zoomRate = 0.05f;
    public Vector2 rotationSpeed = new Vector2(0.4f, 0.4f);
    public Vector2 yLimits = new Vector2(-90f, 90f);
    public Vector2 zoomLimits = new Vector2(10f, 30f);

    private float x = 0.0f;
    private float y = 0.0f;

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

        var activeTouches = new List<TouchControl>();
        foreach (TouchControl touch in Touchscreen.current.touches)
        {
            if (touch.press.isPressed)
            {
                activeTouches.Add(touch);
            }
        }

        if (activeTouches.Count == 1) // Rotate
        {
            TouchControl touch0 = activeTouches[0];

            if (touch0.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Moved)
            {
                Vector2 delta = touch0.delta.ReadValue();
                x += delta.x * rotationSpeed.x;
                y -= delta.y * rotationSpeed.y;
            }
        }
        else if (activeTouches.Count == 2) // ZOOM
        {
            TouchControl touch0 = activeTouches[0];
            TouchControl touch1 = activeTouches[1];

            Vector2 t0Pos = touch0.position.ReadValue();
            Vector2 t1Pos = touch1.position.ReadValue();

            Vector2 t0PrevPos = t0Pos - touch0.delta.ReadValue();
            Vector2 t1PrevPos = t1Pos - touch1.delta.ReadValue();

            float prevMag = (t0PrevPos - t1PrevPos).magnitude;
            float currentMag = (t0Pos - t1Pos).magnitude;

            float diff = prevMag - currentMag;

            distance += diff * zoomRate;
        }

        y = ClampAngle(y, yLimits.x, yLimits.y);
        distance = Mathf.Clamp(distance, zoomLimits.x, zoomLimits.y);

        UpdateCameraPosition();
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