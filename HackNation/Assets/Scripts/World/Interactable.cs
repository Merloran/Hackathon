using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent onClick;
    public void OnClick()
    {
        onClick.Invoke();
    }
}
