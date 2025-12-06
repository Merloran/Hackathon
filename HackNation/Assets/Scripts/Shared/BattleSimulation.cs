using UnityEngine;

public class BattleSimulation : MonoBehaviour
{
    public Health health;
    public Vector2 damageRange = new Vector2(12, 23);
    public float attackCooldownS = 10f;
    private float clock = 0f;

    void Update()
    {
        clock += Time.deltaTime;
        if (clock > attackCooldownS)
        {
            clock = 0f;
            health.ApplyDamage(Random.Range(damageRange.x, damageRange.y));
        }
    }
}
