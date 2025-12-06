using UnityEngine;
using System;

[Serializable]
public class BossSaveData
{
    public string uniqueId;
    public string prefabName;
    public float currentHealth;
    public float maxHealth;
    public Vector3 position;
}

[Serializable]
public class WorldData
{
    public System.Collections.Generic.List<BossSaveData> bosses = new System.Collections.Generic.List<BossSaveData>();
}