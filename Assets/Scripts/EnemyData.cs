using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Combat/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Stats")]
    public int maxHP = 30;
    public int atk = 7;
    public int def = 3;
    public int skill = 5;
    public int iq = 2;
    public int speed = 255;
    public int exp = 3;

    [Header("Moves")]
    public List<Attack> attacks = new List<Attack>();
}
