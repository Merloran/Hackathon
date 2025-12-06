using UnityEngine;
using UnityEngine.SceneManagement;

public class Location : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
