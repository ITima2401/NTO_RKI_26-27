using System;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] DefaultStats defaultStats;
    private void Start()
    {
        if(!defaultStats) { Debug.LogWarning("defaultStats = null");return; }
        if(defaultStats.HP <= 0 || defaultStats.Damage <= 0 || defaultStats.Speed <= 0) 
        { Debug.LogWarning("Uncorrect defaultStats"); return; }

        ChangeHP(new HPAddEvent(defaultStats.HP, defaultStats.HP));
        ChangeDamage(new DamageAddEvent(defaultStats.Damage));
        ChangeSpeed(new SpeedAddEvent(defaultStats.Speed));

        GameStats.Items.Clear();
        if(defaultStats.Items != null && defaultStats.Items.Length != 0)
            foreach (Item item in defaultStats.Items) StatsUpdate(item);
    }
    private void OnEnable()
    {
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) { Debug.LogError("GameBootstrap = null"); return; }
        bus.Subscribe<WaveStartedEvent>(ChangeWave);
        bus.Subscribe<HPAddEvent>(ChangeHP);
        bus.Subscribe<DamageAddEvent>(ChangeDamage);
        bus.Subscribe<SpeedAddEvent>(ChangeSpeed);
        bus.Subscribe<MoneyAddEvent>(ChangeMoney);
        bus.Subscribe<ItemAddEvent>(ChangeItems);
        bus.Subscribe<PlayerObjUpdate>(ChangePlayerObj);

    }
    private void OnDisable()
    {
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) return;
        bus.Unsubscribe<WaveStartedEvent>(ChangeWave);
        bus.Unsubscribe<HPAddEvent>(ChangeHP);
        bus.Unsubscribe<DamageAddEvent>(ChangeDamage);
        bus.Unsubscribe<SpeedAddEvent>(ChangeSpeed);
        bus.Unsubscribe<MoneyAddEvent>(ChangeMoney);
        bus.Unsubscribe<ItemAddEvent>(ChangeItems);
        bus.Unsubscribe<PlayerObjUpdate>(ChangePlayerObj);

    }
    public void StatsUpdate(Item item)
    {
        float finalSpeed = 0;
        float finalDamage = 0;
        int finalMaxHP = 0;
        int finalHP = 0;

        if(item is WeaponInfo weapon) GameStats.Weapons.Add(weapon);
        else if (item is SimpleRelic relic)
        {
            if (relic.HP_value != 0) finalHP += relic.HP_value;
            if (relic.Damage_value != 0) finalDamage += relic.Damage_value;
            if (relic.MaxHP_value != 0) finalMaxHP += relic.MaxHP_value;
            if (relic.Speed_value != 0) finalSpeed += relic.Speed_value;

            if (relic.Damage_mod != 0) finalDamage += (GameStats.Damage + finalDamage) * (relic.Damage_mod / 100);
            if (relic.Speed_mod != 0) finalSpeed += (GameStats.Speed + finalSpeed) * (relic.Speed_mod / 100);
        }
        
        if(finalHP != 0 || finalMaxHP != 0) ChangeHP(new HPAddEvent(finalHP, finalMaxHP));
        if (finalDamage != 0) ChangeDamage(new DamageAddEvent(finalDamage));
        if (finalSpeed != 0) ChangeSpeed(new SpeedAddEvent(finalSpeed));
    }
    private void ChangeHP(HPAddEvent _hp)
    {
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) { Debug.LogError(" GameBootstrap = null"); return; }

        GameStats.MaxHP += _hp.MaxHP;
        GameStats.HP = Math.Min(GameStats.HP+_hp.HP, GameStats.MaxHP);

        bus.Publish(new HPChangedEvent(GameStats.HP, GameStats.MaxHP));
    }
    private void ChangeDamage(DamageAddEvent damage)
    {
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) { Debug.LogError(" GameBootstrap = null"); return; }

        GameStats.Damage += damage.Damage;

        bus.Publish(new DamageChangedEvent(GameStats.Damage));
    }
    private void ChangeSpeed(SpeedAddEvent speed)
    {
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) { Debug.LogError(" GameBootstrap = null"); return; }

        GameStats.Speed += speed.Speed;

        bus.Publish(new SpeedChangedEvent(GameStats.Speed));
    }
    private void ChangeWave(WaveStartedEvent wave)
    {
        GameStats.WaveNum = wave.Number;
    }
    private void ChangeMoney(MoneyAddEvent money)
    {
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) { Debug.LogError(" GameBootstrap = null"); return; }
        if (GameStats.Money + money.Money < 0) { Debug.LogError("Negative money"); return; }

        GameStats.Money += money.Money;
        bus.Publish(new MoneyChangedEvent(GameStats.Money));
    }
    private void ChangePlayerObj(PlayerObjUpdate player)
    { 
        if (player.Player == null) { Debug.LogWarning("Player = null");return;}
        GameStats.Player = player.Player;

    }
    private void ChangeItems(ItemAddEvent item)
    {
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) { Debug.LogError(" GameBootstrap = null"); return; }
        if (item.Item == null) { Debug.LogWarning("item = null"); return; }

        GameStats.Items.Add(item.Item);
        StatsUpdate(item.Item);

        bus.Publish(new ItemsChangedEvent(GameStats.Items.ToArray<Item>()));
    }
}
