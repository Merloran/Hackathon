using UnityEngine;

[CreateAssetMenu(fileName = "CrewmanData", menuName = "Scriptable Objects/CrewmanData")]
public class CrewmanData : ScriptableObject
{
    public string unitName;
    public GameObject visualPrefab;
    public Vector2 damageRange = new Vector2(5, 16);
    public float cooldownS = 1.6f;
}
