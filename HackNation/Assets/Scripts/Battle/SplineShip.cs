using UnityEngine;
using UnityEngine.Splines;
using static UnityEngine.GraphicsBuffer;

public class MoveOnSpline : MonoBehaviour
{
    public SplineContainer spline;
    public float speed = 0.2f;  // jednostki t na sekundê
    float t = 0f;

    void Update()
    {
        if (spline == null || spline.Spline == null) return;

        t += speed * Time.deltaTime;
        t %= 1f;

        // Pobranie pozycji i rotacji
        var pos = spline.EvaluatePosition(t);
        var tangent = spline.EvaluateTangent(t);
        transform.rotation = Quaternion.LookRotation(tangent, Vector3.up);
        transform.rotation *= Quaternion.Euler(0, -270, 0);

        transform.position = pos;
    }
}
