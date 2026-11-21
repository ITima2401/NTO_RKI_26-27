using System;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] DefaultStats defaultStats;
    private void Start()
    {
        foreach(Item item in defaultStats.Items) PlayerStats.Items.Add(item);
        PlayerStats.MaxHP = defaultStats.HP;
        PlayerStats.HP = defaultStats.HP;
        PlayerStats.Damage = defaultStats.Damage;
        PlayerStats.Speed = defaultStats.Speed;
    }
    public void StatsUpdate()
    {
        float finalSpeed = defaultStats.Speed;
        float finalDamage = defaultStats.Damage;
        int finalMaxHP = defaultStats.HP;

        float finalHP = (PlayerStats.HP / PlayerStats.MaxHP) * defaultStats.HP;

        if (PlayerStats.Items.Count > 0)
        {
            float hp_mod = 0;
            float maxHp_mod = 0;
            float damage_mod = 0;
            float speed_mod = 0;

            foreach (var relic in PlayerStats.Items.OfType<SimpleRelic>())
            {
                if (relic.HP_value != 0) finalHP += relic.HP_value;
                if (relic.Damage_value != 0) finalDamage += relic.Damage_value;
                if (relic.MaxHP_value != 0) finalMaxHP += relic.MaxHP_value;
                if (relic.Speed_value != 0) finalSpeed += relic.Speed_value;

                if (relic.HP_mod != 0) hp_mod += relic.HP_mod;
                if (relic.Damage_mod != 0) damage_mod += relic.Damage_mod;
                if (relic.MaxHP_mod != 0) maxHp_mod += relic.MaxHP_mod;
                if (relic.Speed_mod != 0) speed_mod += relic.Speed_mod;
            }

            if (hp_mod != 0) finalHP += finalMaxHP * (1 + (hp_mod / 100));
            if (damage_mod != 0) finalDamage *= 1 + (damage_mod / 100);
            if (maxHp_mod != 0) finalMaxHP = Convert.ToInt32(finalMaxHP * (1 + (maxHp_mod / 100)));
            if (speed_mod != 0) finalSpeed *= 1 + (speed_mod / 100);
        }

        PlayerStats.MaxHP = finalMaxHP;
        PlayerStats.HP = Convert.ToInt32(finalHP);
        PlayerStats.Damage = finalDamage;
        PlayerStats.Speed = finalSpeed;
    }
    private void OnEnable()
    {
        PlayerStats.OnItemsChanged += StatsUpdate;
    }
    private void OnDisable()
    {
        PlayerStats.OnItemsChanged -= StatsUpdate;
    }
}
