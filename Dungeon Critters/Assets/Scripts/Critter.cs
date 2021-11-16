using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Critter", order = 1)]
public class Critter : ScriptableObject
{
    public int id;
    public int health;
    public int maxHealth;
    public int speed;
    public int attack;

    public Sprite sprite;
}
