using UnityEngine;
using UnityEngine.Splines;

public class MoveOnSpline : MonoBehaviour
{
    public SplineContainer spline;
    public float speed = 0.2f;  // jednostki t na sekundê
    float t = 0f;

    void Update()
    {
        if (spline == null || spline.Spline == null) return;

        t += speed * Time.deltaTime;
        t %= 1f; // ruch w kó³ko

        // Pobranie pozycji i rotacji
        var pos = spline.EvaluatePosition(t);
        var rot = spline.EvaluateTangent(t);

        transform.position = pos;
        transform.rotation = Quaternion.LookRotation(rot);
    }
}
