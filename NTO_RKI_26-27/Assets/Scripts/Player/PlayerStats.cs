using System;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    public static GameObject Player;
    public static List<GameObject> Weapons = new List<GameObject>();

    public static event Action<float> OnSpeedChanged;
    private static float speed;
    public static float Speed {  
        get { return speed; } 
        set { speed = value; OnSpeedChanged?.Invoke(value); } 
    }
    public static event Action<float> OnDamageChanged;
    private static float damage;
    public static float Damage
    {
        get { return damage; }
        set { damage = value; OnDamageChanged?.Invoke(value); }
    }
    public static event Action<int> OnHPChanged;
    private static int hp;
    public static int HP
    {
        get { return hp; }
        set { hp = value; OnHPChanged?.Invoke(value); }
    }
    public static event Action<int> OnMaxHPChanged;
    private static int maxHP;
    public static int MaxHP
    {
        get { return maxHP; }
        set { maxHP = value; OnMaxHPChanged?.Invoke(value); }
    }
}
