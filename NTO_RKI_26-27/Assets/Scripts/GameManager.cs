using System;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] DefaultStats defaultStats;
    private void Start()
    {
        if(defaultStats.HP <= 0 || defaultStats.MiliDamage <= 0 ||
            defaultStats.Speed <= 0 || defaultStats.RangeDamage <= 0)
        { Debug.LogWarning("Uncorrect defaultStats"); return; }

        ChangeLuck(new LuckAddEvent(defaultStats.Luck));
        ChangeHP(new HPAddEvent(defaultStats.HP, defaultStats.HP));
        ChangeDamage(new DamageAddEvent(defaultStats.Damage));
        ChangeMiliDamage(new MiliDamageAddEvent(defaultStats.MiliDamage));
        ChangeRangeDamage(new RangeDamageAddEvent(defaultStats.RangeDamage));
        ChangeRegen(new RegenAddEvent(defaultStats.Regen));
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
        bus.Subscribe<MiliDamageAddEvent>(ChangeMiliDamage);
        bus.Subscribe<RangeDamageAddEvent>(ChangeRangeDamage);
        bus.Subscribe<SpeedAddEvent>(ChangeSpeed);
        bus.Subscribe<RegenAddEvent>(ChangeRegen);
        bus.Subscribe<MoneyAddEvent>(ChangeMoney);
        bus.Subscribe<LuckAddEvent>(ChangeLuck);
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
        bus.Unsubscribe<MiliDamageAddEvent>(ChangeMiliDamage);
        bus.Unsubscribe<RangeDamageAddEvent>(ChangeRangeDamage);
        bus.Unsubscribe<SpeedAddEvent>(ChangeSpeed);
        bus.Unsubscribe<RegenAddEvent>(ChangeRegen);
        bus.Unsubscribe<MoneyAddEvent>(ChangeMoney);
        bus.Unsubscribe<LuckAddEvent>(ChangeLuck);
        bus.Unsubscribe<ItemAddEvent>(ChangeItems);
        bus.Unsubscribe<PlayerObjUpdate>(ChangePlayerObj);

    }
    public void StatsUpdate(Item item)
    {

        if(item is WeaponInfo weapon) GameStats.Weapons.Add(weapon);
        else if (item is SimpleRelic relic)
        {

            if (relic.HP != 0 || relic.MaxHP != 0) 
                ChangeHP(new HPAddEvent(relic.HP, relic.MaxHP));
            if (relic.Luck != 0) ChangeLuck(new LuckAddEvent(relic.Luck));
            if (relic.MiliDamage != 0) ChangeMiliDamage(new MiliDamageAddEvent(relic.MiliDamage));
            if (relic.RangeDamage != 0) ChangeRangeDamage(new RangeDamageAddEvent(relic.RangeDamage));
            if (relic.Damage_mod != 0) ChangeDamage(new DamageAddEvent(relic.Damage_mod));
            if (relic.Regen != 0) ChangeRegen(new RegenAddEvent(relic.Regen));
            if (relic.Speed != 0) ChangeSpeed(new SpeedAddEvent(relic.Speed));
        }
    }

    private void ChangeRangeDamage(RangeDamageAddEvent rangeDamage)
    {
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) { Debug.LogError(" GameBootstrap = null"); return; }

        GameStats.RangeDamage += rangeDamage.RangeDamage;

        bus.Publish(new RangeDamageChangeEvent(GameStats.RangeDamage));
    }

    private void ChangeMiliDamage(MiliDamageAddEvent miliDamage)
    {
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) { Debug.LogError(" GameBootstrap = null"); return; }
        
        GameStats.MiliDamage += miliDamage.MiliDamage;

        bus.Publish(new MiliDamageChangeEvent(GameStats.MiliDamage));

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
    private void ChangeRegen(RegenAddEvent regen)
    {
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) { Debug.LogError(" GameBootstrap = null"); return; }

        GameStats.Regen += regen.Regen;

        bus.Publish(new RegenChangedEvent(GameStats.Regen));
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
    private void ChangeLuck(LuckAddEvent luck)
    {
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) { Debug.LogError(" GameBootstrap = null"); return; }
        
        GameStats.Luck += luck.Luck;
        bus.Publish(new LuckChangedEvent(GameStats.Luck));
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
