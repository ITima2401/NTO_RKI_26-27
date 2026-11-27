using UnityEngine;

[CreateAssetMenu(fileName = "SimpleRelic", menuName = "Scriptable Objects/Items/SimpleRelic")]
public class SimpleRelic : Item
{

    public int HP;
    public int MaxHP;
    public int Luck;
    public int MiliDamage;
    public int RangeDamage;
    public int MaxWeaponCount;
    public float Damage_mod;
    public float Regen;
    public float Speed;
    public override string Info()
    {
        string info = "";

        void Append(string label, float value, string suffix = "")
        {
            if (value > 0)
            {
                info += $"{label} +{value.ToString("0.#")}{suffix}\n";
            }
            else if (value < 0)
            {
                info += $"{label} {value.ToString("0.#")}{suffix}\n";
            }
        }

        Append("Здоровье", HP);
        Append("Макс. здоровье", MaxHP);
        Append("Регенерация", Regen, "/сек");
        Append("Скорость", Speed);
        Append("Удача", Luck);
        Append("Ближний урон", MiliDamage);
        Append("Дальний урон", RangeDamage);
        Append("Максимальное кол-во оружий", MaxWeaponCount);
        Append("Урон", Damage_mod, "%");

        return info;
    }
}
