using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CritterAI : MonoBehaviour
{
    public async Task Attack(CritterCombatSlot attacker, CritterCombatSlot target)
    {
        print(123);
        attacker.spriteRenderer.color = Color.green;
        target.spriteRenderer.color = Color.red;
        await Task.Delay(1000);
        attacker.spriteRenderer.color = Color.white;
        target.spriteRenderer.color = Color.white;
        target.TakeDamage(attacker.critter.attack);
        await Task.Yield();
    }
}
