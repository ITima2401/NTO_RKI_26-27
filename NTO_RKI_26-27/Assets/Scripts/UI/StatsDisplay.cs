using TMPro;
using UnityEngine;

public class StatsDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text statsText;

    private void OnEnable()
    {
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) { Debug.LogError("GameBootstrap = null"); return; }

        bus.Subscribe<HPChangedEvent>(OnHPChanged);
        bus.Subscribe<DamageChangedEvent>(OnDamageChanged);
        bus.Subscribe<MiliDamageChangeEvent>(OnMiliDamageChanged);
        bus.Subscribe<RangeDamageChangeEvent>(OnRangeDamageChanged);
        bus.Subscribe<SpeedChangedEvent>(OnSpeedChanged);
        bus.Subscribe<RegenChangedEvent>(OnRegenChanged);
        bus.Subscribe<MoneyChangedEvent>(OnMoneyChanged);
        bus.Subscribe<LuckChangedEvent>(OnLuckChanged);
        bus.Subscribe<MaxWeaponCountChangeEvent>(OnMaxWeaponCountChanged);
        bus.Subscribe<WaveEndedEvent>(OnWaveEnded);

        RefreshText();
    }

    private void OnDisable()
    {
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) return;

        bus.Unsubscribe<HPChangedEvent>(OnHPChanged);
        bus.Unsubscribe<DamageChangedEvent>(OnDamageChanged);
        bus.Unsubscribe<MiliDamageChangeEvent>(OnMiliDamageChanged);
        bus.Unsubscribe<RangeDamageChangeEvent>(OnRangeDamageChanged);
        bus.Unsubscribe<SpeedChangedEvent>(OnSpeedChanged);
        bus.Unsubscribe<RegenChangedEvent>(OnRegenChanged);
        bus.Unsubscribe<MoneyChangedEvent>(OnMoneyChanged);
        bus.Unsubscribe<LuckChangedEvent>(OnLuckChanged);
        bus.Unsubscribe<MaxWeaponCountChangeEvent>(OnMaxWeaponCountChanged);
        bus.Unsubscribe<WaveEndedEvent>(OnWaveEnded);
    }

    // Все хендлеры просто перерисовывают весь текст.
    private void OnHPChanged(HPChangedEvent _) => RefreshText();
    private void OnDamageChanged(DamageChangedEvent _) => RefreshText();
    private void OnMiliDamageChanged(MiliDamageChangeEvent _) => RefreshText();
    private void OnRangeDamageChanged(RangeDamageChangeEvent _) => RefreshText();
    private void OnSpeedChanged(SpeedChangedEvent _) => RefreshText();
    private void OnRegenChanged(RegenChangedEvent _) => RefreshText();
    private void OnMoneyChanged(MoneyChangedEvent _) => RefreshText();
    private void OnLuckChanged(LuckChangedEvent _) => RefreshText();
    private void OnMaxWeaponCountChanged(MaxWeaponCountChangeEvent _) => RefreshText();
    private void OnWaveEnded(WaveEndedEvent _) => RefreshText();
    private void RefreshText()
    {
        if (statsText == null) return;

        string info = "";

        void Append(string label, float value, string suffix = "")
        {
            info += $"{label} {value.ToString("0.#")}{suffix}\n";
        }

        Append("Здоровье", GameStats.HP);
        Append("Макс. здоровье", GameStats.MaxHP);
        Append("Регенерация", GameStats.Regen, "/сек");
        Append("Скорость", GameStats.Speed);
        Append("Удача", GameStats.Luck);
        Append("Ближний урон", GameStats.MiliDamage);
        Append("Дальний урон", GameStats.RangeDamage);
        Append("Урон", GameStats.Damage, "%");
        Append("Деньги", GameStats.Money);
        Append("Макс. кол-во оружий", GameStats.MaxWeaponCount);
        Append("Волна", GameStats.WaveNum);


        statsText.text = info;
    }

}