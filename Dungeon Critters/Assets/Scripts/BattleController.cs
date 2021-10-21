using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public List<CritterStats> critters = new List<CritterStats>();
    public List<CritterStats> t1 = new List<CritterStats>();
    public List<CritterStats> t2 = new List<CritterStats>();

    void Start()
    {
        foreach (CritterStats critter in critters)
        {
            if (critter.enemy)
            {
                t2.Add(critter);
            }
            else
            {
                t1.Add(critter);
            }

            critter.onDeath.AddListener(RemoveCritter);
        }
        print(t1.Count);
        print(t2.Count);
        StartCoroutine(FightRoutine());
    }

    IEnumerator FightRoutine()
    {
        SortBySpeed();
        foreach(CritterStats critter in critters)
        {
            if (critter.dead) { continue; }
            if (!critter.enemy)
            {
                if (t2.Count == 0) { break; }
                CritterStats target = SelectTarget(t2);
                critter.Attack(target);
            }
            else
            {
                if (t1.Count == 0) { break; }
                CritterStats target = SelectTarget(t1);
                critter.Attack(target);
            }

            yield return new WaitForSeconds(0.3f);
        }
        if (t1.Count > 0 && t2.Count > 0)
        {
            StartCoroutine(FightRoutine());
        }
    }

    void SortBySpeed()
    {
        critters.Sort((x, y) => y.speed - x.speed);
    }

    CritterStats SelectTarget(List<CritterStats> possibleTargets)
    {
        for (int i = 0; i < 20; i++)
        {
            int i1 = Random.Range(0, possibleTargets.Count);
            int i2 = Random.Range(0, possibleTargets.Count);

            if (i1 == i2) { continue; }

            CritterStats temp = possibleTargets[i1];
            possibleTargets[i1] = possibleTargets[i2];
            possibleTargets[i2] = temp;
        }

        return possibleTargets[0];
    }


    void RemoveCritter(CritterStats critter)
    {
        if (critter.enemy)
        {
            t2.Remove(critter);
        }
        else
        {
            t1.Remove(critter);
        }
    }
}
