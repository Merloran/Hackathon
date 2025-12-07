using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public Boss bossScript;
    private List<BossData> allBossesData = new List<BossData>();
    private BossData currentBossData;
    private Vector2 totalCrewDamage;
    private float totalCooldownS;

    void Start()
    {
        InitializeBattle();
    }

    void InitializeBattle()
    {
        string targetID = BossSceneData.TargetBossID;

        if (string.IsNullOrEmpty(targetID))
        {
            Debug.LogError("Brak ID bossa!");
            return;
        }

        List<BossSaveData> loadedRawData = SaveSystem.LoadBosses();

        allBossesData.Clear();
        foreach (var raw in loadedRawData)
        {
            BossData newInstance = ScriptableObject.CreateInstance<BossData>();
            newInstance.LoadFromSaveData(raw);
            allBossesData.Add(newInstance);
        }

        currentBossData = allBossesData.FirstOrDefault(b => b.uniqueId == targetID);

        if (currentBossData != null)
        {
            bossScript.Initialize(currentBossData);
            bossScript.OnHealthChanged += (isPlayer) =>
            {
                SaveSystem.SaveBosses(allBossesData);
            };

            bossScript.OnDeath += (deadBoss) =>
            {
                OnBossDefeated();
            };
        }
        else
        {
            Debug.LogError("Nie znaleziono bossa o takim ID w wczytanych danych!");
        }
    }

    void OnBossDefeated()
    {
        if (currentBossData != null)
        {
            allBossesData.Remove(currentBossData);
        }

        SaveAndReturn();
    }

    public void SaveAndReturn()
    {
        SaveSystem.SaveBosses(allBossesData);

        SceneManager.LoadScene("WorldScene");
    }
}