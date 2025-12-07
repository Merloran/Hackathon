using UnityEngine;

public class BattleSimulation : MonoBehaviour
{
    public Boss boss;
    public Vector2 damageRange = new Vector2(12, 23);
    public float attackCooldownS = 1.5f;
    private float clock = 0f;
    public bool isPlayer = false;

    void Update()
    {
        clock += Time.deltaTime;
        if (clock > attackCooldownS)
        {
            clock = 0f;
            boss.ApplyDamage(Random.Range(damageRange.x, damageRange.y), isPlayer);
        }
    }
}
