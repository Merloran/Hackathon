using UnityEngine;

public class CrewButton : MonoBehaviour
{
    public GameObject crewMenu;
    public GameObject hud;
    public WorldCamera worldCamera;

    public void OpenCrewMenu()
    {
        worldCamera.isInUI = true;
        crewMenu.SetActive(true);
        hud.SetActive(false);
    }
}
