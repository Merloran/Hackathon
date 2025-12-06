using UnityEngine;

public class BossData : ScriptableObject
{
    public string uniqueId;
    public string prefabName;
    public float maxHealth;
    public float currentHealth;
    public Vector3 position;

    public BossSaveData ToSaveData()
    {
        return new BossSaveData
        {
            uniqueId = uniqueId,
            prefabName = prefabName,
            currentHealth = currentHealth,
            maxHealth = maxHealth,
            position = position
        };
    }

    public void LoadFromSaveData(BossSaveData save)
    {
        uniqueId = save.uniqueId;
        prefabName = save.prefabName;
        currentHealth = save.currentHealth;
        maxHealth = save.maxHealth;
        position = save.position;
    }
}