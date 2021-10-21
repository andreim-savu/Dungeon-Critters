using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class CritterEvent : UnityEvent<CritterStats>
{
}


public class CritterStats : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public int attack;
    public int speed;
    public bool enemy;

    public bool dead;

    public Slider healthSlider;

    public CritterEvent onDeath = new CritterEvent();

    private void Awake()
    {
        maxHealth = Random.Range(100, 200);
        health = maxHealth;
        attack = Random.Range(5, 10);
        speed = Random.Range(10, 100);
    }

    public void Attack(CritterStats target)
    {
        StartCoroutine(AttackRoutine(target));
    }

    IEnumerator AttackRoutine(CritterStats target)
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
        target.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
        target.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
        target.TakeDamage(attack);
    }

    public void TakeDamage(int amount)
    {
        health -= Mathf.Max(0, amount);
        UpdateHealthSlider();
        if (health <= 0) {
            onDeath.Invoke(this);
            dead = true;
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }

    void UpdateHealthSlider()
    {
        float percentage = health / (float)maxHealth;
        healthSlider.value = percentage;
    }
}
