// Событие: волна началась. Публикует WaveManager.
using UnityEngine;

public readonly struct WaveStartedEvent
{
    public readonly WaveData Wave;
    public readonly int Number;
    public WaveStartedEvent(WaveData wave, int number) { Wave = wave; Number = number; }
}

// Событие: волна закончилась. Публикует Spawner.
public readonly struct WaveEndedEvent
{
    public readonly int Number;
    public WaveEndedEvent(int number) { Number = number; }
}
// Событие: изменился объект игрока.
public readonly struct PlayerObjUpdate
{
    public readonly GameObject Player;
    public PlayerObjUpdate(GameObject player) { Player = player; }
}
// Событие: изменилось здоровье игрока.
public readonly struct HPChangedEvent
{
    public readonly int HP;
    public readonly int MaxHP;
    public HPChangedEvent(int hp, int maxHp) { HP = hp; MaxHP = maxHp; }
}
// Событие: удаленно оружие игрока.
public readonly struct WeaponSubtractEvent
{
    public readonly WeaponInfo Weapon;
    public WeaponSubtractEvent(WeaponInfo weapon) { Weapon = weapon; }
}
// Событие: изменились предметы игрока.
public readonly struct ItemsChangedEvent
{
    public readonly Item[] Item;
    public ItemsChangedEvent(Item[] item) { Item = item; }
}
// Событие: изменился ближний урон игрока.
public readonly struct MiliDamageChangeEvent
{
    public readonly int MiliDamage;
    public MiliDamageChangeEvent(int miliDamage) { MiliDamage = miliDamage; }
}
// Событие: изменился дальний урон игрока.
public readonly struct RangeDamageChangeEvent
{
    public readonly int RangeDamage;
    public RangeDamageChangeEvent(int damage) { RangeDamage = damage; }
}

// Событие: изменилась скорость игрока.
public readonly struct SpeedChangedEvent
{
    public readonly float Speed;
    public SpeedChangedEvent(float speed) { Speed = speed; }
}
// Событие: изменилась скорость регенерации игрока.
public readonly struct RegenChangedEvent
{
    public readonly float Regen;
    public RegenChangedEvent(float regen) { Regen = regen; }
}
// Событие: изменилось кол-во денег(кнопок).
public readonly struct MoneyChangedEvent
{
    public readonly int Money;
    public MoneyChangedEvent(int money) { Money = money; }
}
// Событие: добавилась удача.
public readonly struct LuckChangedEvent
{
    public readonly int Luck;
    public LuckChangedEvent(int luck) { Luck = luck; }
}
// Событие: изменилось макс. кол-во  оружий.
public readonly struct MaxWeaponCountChangeEvent
{
    public readonly int MaxWeaponCount;
    public MaxWeaponCountChangeEvent(int maxWeaponCount) { MaxWeaponCount = maxWeaponCount; }
}
// Событие: изменился урон игрока. 
public readonly struct DamageChangedEvent
{
    public readonly float Damage;
    public DamageChangedEvent(float damage) { Damage = damage; }
}
// Событие: добавился предмет. 
public readonly struct ItemAddEvent
{
    public readonly Item Item;
    public ItemAddEvent(Item item) { Item = item; }
}
// Событие: добавилось здоровье игрока.
public readonly struct HPAddEvent
{
    public readonly int HP;
    public readonly int MaxHP;
    public HPAddEvent(int hp,int maxHP) { HP = hp; MaxHP = maxHP;}
}
// Событие: добавился ближний урон игрока.
public readonly struct MiliDamageAddEvent
{
    public readonly int MiliDamage;
    public MiliDamageAddEvent(int miliDamage) { MiliDamage = miliDamage; }
}
// Событие: добавился дальний урон игрока.
public readonly struct RangeDamageAddEvent
{
    public readonly int RangeDamage;
    public RangeDamageAddEvent(int damage) { RangeDamage = damage; }
}
// Событие: добавилась скорость игроку.
public readonly struct SpeedAddEvent
{
    public readonly float Speed;
    public SpeedAddEvent(float speed) { Speed = speed; }
}
// Событие: изменилась скорость регенерации игрока.
public readonly struct RegenAddEvent
{
    public readonly float Regen;
    public RegenAddEvent(float regen) { Regen = regen; }
}

// Событие: добавился урон игрока. 
public readonly struct DamageAddEvent
{
    public readonly float Damage;
    public DamageAddEvent(float damage) { Damage = damage; }
}
// Событие: добавились денеги(кнопоки).
public readonly struct MoneyAddEvent
{
    public readonly int Money;
    public MoneyAddEvent(int money) { Money = money; }
}
// Событие: добавилась удача.
public readonly struct LuckAddEvent
{
    public readonly int Luck;
    public LuckAddEvent(int luck) { Luck = luck; }
}
// Событие: добавилось макс. кол-во  оружий.
public readonly struct MaxWeaponCountAddEvent
{
    public readonly int MaxWeaponCount;
    public MaxWeaponCountAddEvent(int maxWeaponCount) { MaxWeaponCount = maxWeaponCount; }
}