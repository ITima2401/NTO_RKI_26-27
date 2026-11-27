using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameStats
{
    //Fire Damage
    //Lighning Damage

    //Attack Speed
    //Range

    //Crit Chance
    //Crit Damage

    public static int Luck;
    public static int HP;
    public static int MaxHP;
    public static int Money;
    public static int WaveNum;
    public static int MaxWeaponCount;
    public static float Regen;
    public static float Speed;
    public static int MiliDamage;
    public static int RangeDamage;
    public static float Damage;
    public static GameObject Player;
    public static List<Item> Items = new List<Item>();
    public static List<WeaponInfo> Weapons = new List<WeaponInfo>();
}
