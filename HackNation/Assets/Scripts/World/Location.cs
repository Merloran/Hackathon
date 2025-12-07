using UnityEngine;
using UnityEngine.SceneManagement;

public class Location : MonoBehaviour
{
    [SerializeField] private string sceneName;
    private BossData bossData;

    private void Start()
    {
        Boss bd;
        gameObject.TryGetComponent<Boss>(out bd);
        if (bd != null)
        {
            bossData = bd.data;
        }
        else
        {
            bossData = null;
        }
    }

    public void LoadScene()
    {
        if (bossData != null)
        {
            BossSceneData.TargetBossID = bossData.uniqueId;
        }

        SceneManager.LoadScene(sceneName);
    }
}
