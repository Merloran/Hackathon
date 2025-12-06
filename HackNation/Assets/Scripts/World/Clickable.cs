using UnityEngine;
using UnityEngine.Events;

public class Clickable : MonoBehaviour
{
    public UnityEvent onClick;
    public void OnClick()
    {
        onClick.Invoke();
    }
}
