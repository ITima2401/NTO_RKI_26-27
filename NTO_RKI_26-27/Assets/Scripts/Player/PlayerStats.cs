using System;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    public static GameObject Player;
    public static List<WeaponInfo> Weapons = new List<WeaponInfo>();
    public static int Money;
    public static event Action OnItemsChanged;
    private static List<Item> items = new List<Item>();
    public static List<Item> Items
    {
        get { return items; }
        set { items = value; OnItemsChanged?.Invoke(); }
    }
    public static void AddItem(Item item)
    {
        items.Add(item);
        OnItemsChanged?.Invoke();
    }
    public static event Action<float> OnSpeedChanged;
    private static float speed;
    public static float Speed {  
        get => speed;
        set { speed = value; OnSpeedChanged?.Invoke(value); } 
    }
    public static event Action<float> OnDamageChanged;
    private static float damage;
    public static float Damage
    {
        get => damage;
        set { damage = value; OnDamageChanged?.Invoke(value); }
    }

    public static event Action<int> OnWaveNumChanged;
    private static int waveNum;
    public static int WaveNum
    {
        get => waveNum;
        set { waveNum = value; OnWaveNumChanged?.Invoke(value); }
    }
    public static event Action<int> OnHPChanged;
    private static int hp;
    public static int HP
    {
        get => hp;
        set
        {
            int clamped = Mathf.Min(value, maxHP);
            if (hp == clamped) return;
            hp = clamped;
            OnHPChanged?.Invoke(hp);
        }
    }
    public static event Action<int> OnMaxHPChanged;
    private static int maxHP;
    public static int MaxHP
    {
        get => maxHP;
        set
        {
            maxHP = value;
            if (hp > maxHP) { hp = maxHP; OnHPChanged?.Invoke(hp); }
            OnMaxHPChanged?.Invoke(maxHP);
        }
    }
}
