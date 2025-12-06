using UnityEngine;
using UnityEngine.SceneManagement;

public class Location : MonoBehaviour
{
    [SerializeField] private string sceneName;
    private BossData bossData;

    private void Start()
    {
        bossData = gameObject.GetComponent<Boss>().data;
    }

    public void LoadScene()
    {
        BossSceneData.TargetBossID = bossData.uniqueId;

        SceneManager.LoadScene(sceneName);
    }
}
