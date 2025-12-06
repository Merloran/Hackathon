using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BossSpawner : MonoBehaviour
{
    public List<GameObject> bossPrefabs;
    public Transform worldTransform;
    public float radius = 10f;
    public Vector2 healthRange = new Vector2(40f, 200f);
    public float spawnCooldown = 1f;
    public int maxBossCount = 10;

    private List<BossData> runtimeBosses = new List<BossData>();
    private float clock = 0f;

    void Start()
    {
        LoadGame();
    }

    void Update()
    {
        // 1. Obs³uga spawnowania
        if (runtimeBosses.Count < maxBossCount)
        {
            clock += Time.deltaTime;
            if (clock >= spawnCooldown)
            {
                SpawnNewBoss();
                clock = 0f;
            }
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveGame();
        }
    }

    void OnApplicationQuit()
    {
        SaveGame();
    }

    void SaveGame()
    {
        SaveSystem.SaveBosses(runtimeBosses);
    }

    void LoadGame()
    {
        List<BossSaveData> loadedData = SaveSystem.LoadBosses();
        foreach (var saveData in loadedData)
        {
            GameObject prefabToSpawn = bossPrefabs.FirstOrDefault(p => p.name == saveData.prefabName);
            if (prefabToSpawn != null)
            {
                BossData dataInstance = ScriptableObject.CreateInstance<BossData>();
                dataInstance.LoadFromSaveData(saveData);
                runtimeBosses.Add(dataInstance);
                InstantiateBossObject(prefabToSpawn, dataInstance);
            }
        }
    }

    void SpawnNewBoss()
    {
        if (bossPrefabs.Count == 0) return;

        Vector3 randomPos = Vector3.Scale(Random.onUnitSphere * radius, worldTransform.localScale)
                          + worldTransform.position;
        GameObject selectedPrefab = bossPrefabs[Random.Range(0, bossPrefabs.Count)];

        BossData newData = ScriptableObject.CreateInstance<BossData>();
        newData.uniqueId = System.Guid.NewGuid().ToString();
        newData.prefabName = selectedPrefab.name;
        newData.maxHealth = Random.Range(healthRange.x, healthRange.y);
        newData.currentHealth = newData.maxHealth;
        newData.position = randomPos;

        runtimeBosses.Add(newData);
        InstantiateBossObject(selectedPrefab, newData);
    }

    void InstantiateBossObject(GameObject prefab, BossData data)
    {
        GameObject obj = Instantiate(prefab, data.position, Quaternion.identity);
        obj.transform.up = (data.position - worldTransform.position).normalized;
        Boss bossScript = obj.GetComponent<Boss>();
        bossScript.Initialize(data);
        bossScript.OnHealthChanged += () => { OnBossDataChanged(); };

        bossScript.OnDeath += (deadBoss) => { OnBossDied(deadBoss.data); };
        SaveGame();
    }

    public void OnBossDataChanged()
    {
        SaveGame();
    }

    public void OnBossDied(BossData dataToRemove)
    {
        if (runtimeBosses.Contains(dataToRemove))
        {
            runtimeBosses.Remove(dataToRemove);
            SaveGame();
        }
    }
}