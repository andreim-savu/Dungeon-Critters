using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


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

    void UpdateHealthSlider()
    {
        float percentage = health / (float)maxHealth;
        healthSlider.value = percentage;
    }
}
