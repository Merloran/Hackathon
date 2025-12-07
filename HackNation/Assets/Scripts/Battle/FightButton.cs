using UnityEngine;

public class FightButton : MonoBehaviour
{
    public Boss boss;
    public Crew crew;
    public GameObject UI;
    void Update()
    {
        
    }

    public void OnClick()
    {
        UI.SetActive(false);
        var sim = crew.gameObject.AddComponent<BattleSimulation>();
        sim.damageRange = crew.crewDamage;
        sim.attackCooldownS = crew.crewCooldownS;
        sim.boss = boss;
    }
}
