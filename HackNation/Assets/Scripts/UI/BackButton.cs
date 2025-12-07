using UnityEngine;

public class BackButton : MonoBehaviour
{
    public GameObject hudMenu;
    public GameObject crewMenu;
    public WorldCamera worldCamera;
    
    public void CloseCrewMenu()
    {
        worldCamera.isInUI = false;
        hudMenu.SetActive(true);
        crewMenu.SetActive(false);
    }
}
