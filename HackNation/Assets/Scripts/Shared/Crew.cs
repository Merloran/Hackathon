using System.Collections.Generic;
using UnityEngine;


public class Crew : MonoBehaviour
{
    public List<Transform> spawnPoints = new List<Transform>();
    public List<CrewmanData> initialCrew = new List<CrewmanData>();
    List<GameObject> spawned = new List<GameObject>();
    public RuntimeAnimatorController animatorController;

    public Vector2 crewDamage;
    public float crewCooldownS;

    void Start()
    {
        if (BossSceneData.SelectedCrew.Count > 0)
        {
            CalculateValues();
        }
        else
        {
            for (int i = 0; i < initialCrew.Count; ++i)
            {
                BossSceneData.SelectedCrew.Add(initialCrew[i]);
            }
            BossSceneData.crewmanToChange = initialCrew[0];
        }
        SpawnCrew();
    }


    void SpawnCrew()
    {
        for (int i = 0; i < spawned.Count; i++)
        {
            Destroy(spawned[i]);
        }
        spawned.Clear();

        List<CrewmanData> crewToSpawn = BossSceneData.SelectedCrew;

        if (crewToSpawn == null || crewToSpawn.Count == 0)
        {
            Debug.LogError("Brak wybranej za³ogi w CrewTransferData!");
            return;
        }

        crewDamage = Vector2.zero;
        crewCooldownS = 0;

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            CrewmanData data = crewToSpawn[i];

            crewDamage += data.damageRange;
            crewCooldownS += data.cooldownS;

            if (data.visualPrefab != null)
            {
                var obj = Instantiate(data.visualPrefab, spawnPoints[i].position, spawnPoints[i].rotation, spawnPoints[i]);
                var animator = obj.GetComponent<Animator>();
                animator.runtimeAnimatorController = animatorController;
                animator.SetInteger("animationIndex", 1);
                animator.applyRootMotion = false;
                spawned.Add(obj);
            }
        }

        crewCooldownS /= crewToSpawn.Count;
    }

    public void SwapCrew(CrewmanData previous, CrewmanData current)
    {
        for (int i = 0; i < BossSceneData.SelectedCrew.Count; i++)
        {
            if (previous == BossSceneData.SelectedCrew[i])
            {
                BossSceneData.SelectedCrew[i] = current;
                BossSceneData.crewmanToChange = current;
                break;
            }
        }

        SpawnCrew();
    }

    public void AnimateCrew()
    {
        for (int i = 0; i < spawned.Count; i++)
        {
            var animator = spawned[i].GetComponent<Animator>();
            animator.SetInteger("animationIndex", Random.Range(2, 5));
            animator.SetTrigger("ChangeAnimation");
        }
    }

    void CalculateValues()
    {
        crewDamage = Vector2.zero;
        crewCooldownS = 0;
        for (int i = 0; i < BossSceneData.SelectedCrew.Count; i++)
        {
            CrewmanData man = BossSceneData.SelectedCrew[i];
            crewDamage += man.damageRange;
            crewCooldownS += man.cooldownS;
        }
        crewCooldownS /= BossSceneData.SelectedCrew.Count;
    }
}
