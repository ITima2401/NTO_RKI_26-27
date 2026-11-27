using UnityEngine;

[CreateAssetMenu(fileName = "WeaponInfo", menuName = "Scriptable Objects/Items/WeaponInfo")]
public class WeaponInfo : Item
{
    public GameObject WeaponPrefab;
    public float Damage;
    public float Recharge;
    public bool isChase = true;
    public bool isRange;
    public float Range;
    [Header("Только для Range Weapon")]
    public GameObject Bullet;
    public float BulletRange;
    public float BulletSpeed;

    public override string Info()
    {
        string info = "";

        void Append(string label, float value, string suffix = "")
        {
            info += $"{label} {value.ToString("0.#")}{suffix}\n";
        }

        if (isRange) info += "Тип: дальний бой\n";
        else info += "Тип: ближний бой\n";

        Append("Урон", Damage);
        Append("Перезарядка", Recharge, " сек");
        Append("Дальность", Range);
        if (isRange) Append("Скорость пули", BulletSpeed);

        return info;
    }
    public override bool isLegal()
    {
        return GameStats.Money >= Cost && GameStats.Weapons.Count < GameStats.MaxWeaponCount && WeaponPrefab != null;
    }
}