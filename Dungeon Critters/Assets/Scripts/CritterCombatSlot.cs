using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class CritterEvent : UnityEvent<CritterCombatSlot>
{
}

public class CritterCombatSlot : MonoBehaviour
{
    public Critter critter;
    public CritterAI ai;

    public SpriteRenderer spriteRenderer;

    public Slider healthSlider;

    public bool dead;
    public bool enemyCritter;

    public CritterEvent onDeath = new CritterEvent();

    void Start()
    {
        critter = Instantiate(critter);
        ai = GetComponent<CritterAI>();
        spriteRenderer.sprite = critter.sprite;
    }

    public async void Attack(CritterCombatSlot target)
    {
        await ai.Attack(this, target);
    }

    public void TakeDamage(int amount)
    {
        critter.health -= Mathf.Max(0, amount);
        UpdateHealthSlider();
        if (critter.health <= 0)
        {
            onDeath.Invoke(this);
            dead = true;
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }

    void UpdateHealthSlider()
    {
        float percentage = critter.health / (float)critter.maxHealth;
        healthSlider.value = percentage;
    }
}
