using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameStats
{
    public static int HP;
    public static int MaxHP;
    public static int Money;
    public static int WaveNum;
    public static float Speed;
    public static float Damage;
    public static GameObject Player;
    public static List<Item> Items = new List<Item>();
    public static List<WeaponInfo> Weapons = new List<WeaponInfo>();
}
