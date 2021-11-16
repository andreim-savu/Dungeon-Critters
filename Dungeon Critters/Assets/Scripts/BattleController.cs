using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public List<CritterCombatSlot> critters = new List<CritterCombatSlot>();
    public List<CritterCombatSlot> t1 = new List<CritterCombatSlot>();
    public List<CritterCombatSlot> t2 = new List<CritterCombatSlot>();

    void Start()
    {
        foreach (CritterCombatSlot critter in critters)
        {
            if (critter.enemyCritter)
            {
                t2.Add(critter);
            }
            else
            {
                t1.Add(critter);
            }

            critter.onDeath.AddListener(RemoveCritter);
        }

    }

    async void CombatTurn()
    {
        SortBySpeed();
        foreach (CritterCombatSlot critter in critters)
        {
            if (critter.dead) { continue; }
            CritterCombatSlot target;
            if (!critter.enemyCritter)
            {
                if (t2.Count == 0) { break; }
                target = SelectTarget(t2);
            }
            else
            {
                if (t1.Count == 0) { break; }
                target = SelectTarget(t1);
            }
            await critter.ai.Attack(critter, target);
        }
    }

    void SortBySpeed()
    {
        critters.Sort((x, y) => y.critter.speed - x.critter.speed);
    }

    CritterCombatSlot SelectTarget(List<CritterCombatSlot> possibleTargets)
    {
        for (int i = 0; i < 20; i++)
        {
            int i1 = Random.Range(0, possibleTargets.Count);
            int i2 = Random.Range(0, possibleTargets.Count);

            if (i1 == i2) { continue; }

            CritterCombatSlot temp = possibleTargets[i1];
            possibleTargets[i1] = possibleTargets[i2];
            possibleTargets[i2] = temp;
        }

        return possibleTargets[0];
    }


    void RemoveCritter(CritterCombatSlot critter)
    {
        if (critter.enemyCritter)
        {
            t2.Remove(critter);
        }
        else
        {
            t1.Remove(critter);
        }
    }

    private void OnMouseDown()
    {
        CombatTurn();
    }
}
