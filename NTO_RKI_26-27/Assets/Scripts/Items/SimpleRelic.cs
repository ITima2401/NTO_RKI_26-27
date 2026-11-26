using UnityEngine;

[CreateAssetMenu(fileName = "SimpleRelic", menuName = "Scriptable Objects/Items/SimpleRelic")]
public class SimpleRelic : Item
{

    public int HP;
    public int MaxHP;
    public int Luck;
    public int MiliDamage;
    public int RangeDamage;
    public float Damage_mod;
    public float Regen;
    public float Speed;
    public override string Info()
    {
        string info = "";

        void Append(string label, float value, string suffix = "")
        {
            if (value > 0) info += $"{label} +{value.ToString("0.#")}{suffix}\n";
            else if (value < 0) info += $"{label} {value.ToString("0.#")}{suffix}\n";
        }
        //ПЕРЕДЕЛАТЬ
        Append("Здоровье", HP);
        Append("Макс. здоровье", MaxHP);
        Append("Скорость", Speed);
        Append("Урон", Damage_mod, "%");

        return info;
    }
}
