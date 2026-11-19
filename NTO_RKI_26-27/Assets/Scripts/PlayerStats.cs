using System;
using UnityEngine;

public static class PlayerStats
{
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
    public static event Action<float> OnHPChanged;
    private static float hp;
    public static float HP
    {
        get { return hp; }
        set { hp = value; OnHPChanged?.Invoke(value); }
    }
    public static event Action<float> OnMaxHPChanged;
    private static float maxHP;
    public static float MaxHP
    {
        get { return maxHP; }
        set { maxHP = value; OnMaxHPChanged?.Invoke(value); }
    }

}
