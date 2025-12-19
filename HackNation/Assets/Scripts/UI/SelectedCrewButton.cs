using UnityEngine;

public class SelectedCrewButton : MonoBehaviour
{
    public int index;
    public bool isInitial = false;

    private void Start()
    {
        if (isInitial)
        {
            BossSceneData.currentCrewImage = GetComponentInChildren<UnityEngine.UI.Image>();
        }
    }
}
