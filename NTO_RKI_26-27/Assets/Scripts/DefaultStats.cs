using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultStats", menuName = "Scriptable Objects/DefaultStats")]
public class DefaultStats : ScriptableObject
{
    public int HP;
    public int Luck;
    public int MiliDamage;
    public int RangeDamage;
    public float Damage;
    public float Regen;
    public float Speed;

    public Item[] Items;
}
