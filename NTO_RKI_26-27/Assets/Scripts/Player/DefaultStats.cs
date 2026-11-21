using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DefaultStats : ScriptableObject
{
    public int HP;
    public float Speed;
    public float Damage;

    public List<Item> Items = new List<Item>();
}
