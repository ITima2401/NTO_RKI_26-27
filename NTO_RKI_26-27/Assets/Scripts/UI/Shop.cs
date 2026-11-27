using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Shop : MonoBehaviour
{
    [SerializeField] int RefStartPrise;
    [SerializeField] AllItems AllItems;
    [SerializeField] GameObject Menu;
    [SerializeField] TMP_Text RefPrise;
    [SerializeField] Button RefBut;
    [SerializeField] Button[] buttons;
    [SerializeField] TMP_Text[] Infs;
    [SerializeField] ScrollRect[] Scrolls;
    [SerializeField] TMP_Text[] Prise;
    [SerializeField] Image[] Panels;
    [SerializeField] Image[] Sprite;
    private Item[] Items = new Item[3];
    private int waveNum;
    private int refCount;
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

    private void OnWaveEnded(WaveEndedEvent _) { waveNum = _.Number; Refresh(); }
    private void OnWaveStarted(WaveStartedEvent _) { Menu.SetActive(false); refCount = 0; }

    public void Buy(int buttonID)
    {
        if (Items[buttonID] == null) return;
        if (Items[buttonID].isLegal())
        {
            var bus = GameBootstrap.Instance?.Bus;
            if (bus == null) { Debug.LogError("GameBootstrap = null"); return; }
            bus.Publish<MoneyAddEvent>(new MoneyAddEvent(-Items[buttonID].Cost));
            bus.Publish<ItemAddEvent>(new ItemAddEvent(Items[buttonID]));
            Panels[buttonID].gameObject.SetActive(false);
            for (int i = 0; i < buttons.Length; i++)
            {
                if(buttons[i] == null) continue;
                buttons[i].interactable = Items[i].isLegal();
            }
            RefBut.interactable = GameStats.Money >= RefStartPrise * refCount * refCount;
        }
    }

    private void ShowInfo(int index)
    {
        Infs[index].text = Items[index].Info();
        Infs[index].ForceMeshUpdate();

        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(Infs[index].rectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(Scrolls[index].content);
        Canvas.ForceUpdateCanvases();

        Scrolls[index].vertical = true;
        Scrolls[index].verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHide;
        Scrolls[index].verticalNormalizedPosition = 1f;
    }

    public void Refresh()
    {

        if (AllItems == null) return;
        if (AllItems.pools == null) return;
        foreach (var item in AllItems.pools)
        {
            if (item == null || item.Items == null || item.Items.Length == 0)
            {
                Debug.LogWarning("pool is empty");
                return;
            }
        }
        Menu.SetActive(true);
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) { Debug.LogError("GameBootstrap = null"); return; }
        if(GameStats.Money >= RefStartPrise * refCount * refCount)
            bus.Publish<MoneyAddEvent>(new MoneyAddEvent(-(RefStartPrise * refCount * refCount)));
        refCount++;
        RefBut.interactable = GameStats.Money >= RefStartPrise * refCount * refCount;
        RefPrise.text = $"{RefStartPrise * refCount * refCount}";
        AllItems.PoolsSort();
        float[] Chances = new float[AllItems.pools.Length];
        for (int i = 0; i < AllItems.pools.Length; i++)
        {
            if (waveNum < AllItems.pools[i].MinWave) { Chances[i] = 0; continue; }
            Chances[i] = Mathf.Min((AllItems.pools[i].BaseChance
                + AllItems.pools[i].GrowPerWave * (waveNum - AllItems.pools[i].MinWave))
                * (1 + Mathf.Max(GameStats.Luck, -99) / 100f), AllItems.pools[i].MaxChance);

        }
        for (int i = 0; i < Items.Length; i++)
        {
            Panels[i].gameObject.SetActive(true);
            float D100 = UnityEngine.Random.Range(0f, 100f);
            for (int j = 0; j < AllItems.pools.Length; j++)
            {
                if (D100 < Chances[j])
                {
                    Items[i] = AllItems.pools[j].Items[UnityEngine.Random.Range(0, AllItems.pools[j].Items.Length)];
                    ShowInfo(i);
                    Prise[i].text = $"{Items[i].Cost}";
                    Sprite[i].sprite = Items[i].sprite;
                    Panels[i].sprite = AllItems.pools[j].BackGround;
                    break;
                }
                if (j == AllItems.pools.Length - 1)
                {
                    Items[i] = AllItems.pools[j].Items[UnityEngine.Random.Range(0, AllItems.pools[j].Items.Length)];
                    ShowInfo(i);
                    Prise[i].text = $"{Items[i].Cost}";
                    Sprite[i].sprite = Items[i].sprite;
                    Panels[i].sprite = AllItems.pools[j].BackGround;
                    break;
                }
            }
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] == null) continue;
            buttons[i].interactable = Items[i].isLegal();
        }
    }
}