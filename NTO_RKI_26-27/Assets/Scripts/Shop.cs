using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public AllItems AllItems;
    [SerializeField] GameObject Menu;
    public TMP_Text[] Infs;
    public TMP_Text[] Prise;
    public Image[] Panels;
    public Image[] Sprite;
    public Item[] Items = new Item[3];

    private void OnEnable()
    {
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) { Debug.LogError("GameBootstrap = null"); return; }
        bus.Subscribe<WaveEndedEvent>(OnWaveEnded);
        bus.Subscribe<WaveStartedEvent>(OnWaveStarted);
    }

    private void OnDisable()
    {
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) return;
        bus.Unsubscribe<WaveEndedEvent>(OnWaveEnded);
        bus.Unsubscribe<WaveStartedEvent>(OnWaveStarted);
    }

    private void OnWaveEnded(WaveEndedEvent _) => Refresh();
    private void OnWaveStarted(WaveStartedEvent _) => Menu.SetActive(false);

    public void Buy(int buttonID)
    {
        if (Items[buttonID] == null) return;
        if (GameStats.Money >= Items[buttonID].Cost)
        {
            var bus = GameBootstrap.Instance?.Bus;
            if (bus == null) { Debug.LogError("GameBootstrap = null"); return; }
            bus.Publish<MoneyAddEvent>(new MoneyAddEvent(-Items[buttonID].Cost));
            bus.Publish<ItemAddEvent>(new ItemAddEvent(Items[buttonID]));
            Panels[buttonID].gameObject.SetActive(false);

        }
    }

    public void Refresh()
    {
        if (AllItems == null) return;
        if (AllItems.ItemsLvl1 == null || AllItems.ItemsLvl1.Length == 0)
        {
            Debug.LogWarning($"{nameof(Shop)}: ItemsLvl1 ÔÛÒÚ.");
            return;
        }// œ≈–≈ƒ≈À¿“‹

        Menu.SetActive(true);

        for (int i = 0; i < Items.Length; i++)
        {
            Panels[i].gameObject.SetActive(true);
            Items[i] = AllItems.ItemsLvl1[Random.Range(0, AllItems.ItemsLvl1.Length)];
            Infs[i].text = Items[i].Info();
            Prise[i].text = $"{Items[i].Cost}";
            Panels[i].sprite = AllItems.Lvl1_BackGround;
            Sprite[i].sprite = Items[i].sprite;
        }
    }
}