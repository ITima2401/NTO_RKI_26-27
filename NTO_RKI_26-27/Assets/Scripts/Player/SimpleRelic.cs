using UnityEngine;

[CreateAssetMenu]
public class SimpleRelic : Item
{

    public int HP_value;
    public float HP_mod;
    
    public int MaxHP_value;
    public float MaxHP_mod;
    
    public float Speed_value;
    public float Speed_mod;

    public float Damage_value;
    public float Damage_mod;
    public override string Info()
    {
        string info = "";

        void Append(string label, float value, string suffix = "")
        {
            if (value > 0) info += $"{label} +{value.ToString("0.#")}{suffix}\n";
            else if (value < 0) info += $"{label} {value.ToString("0.#")}{suffix}\n";
        }

        Append("Здоровье", HP_value);
        Append("Здоровье", HP_mod, "%");
        Append("Макс. здоровье", MaxHP_value);
        Append("Макс. здоровье", MaxHP_mod, "%");
        Append("Скорость", Speed_value);
        Append("Скорость", Speed_mod, "%");
        Append("Урон", Damage_value);
        Append("Урон", Damage_mod, "%");

        return info;
    }
}
