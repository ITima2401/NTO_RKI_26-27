using System;
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
    private int waveNum;
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

    private void OnWaveEnded(WaveEndedEvent _) { waveNum = _.Number;  Refresh();}
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

        Menu.SetActive(true);
        if (AllItems == null) return;
        foreach (var item in AllItems.pools)
        {
            if (item.Items == null || item.Items.Length == 0)
            {
                Debug.LogWarning("pool is empty");
                return;
            }
        }
        AllItems.PoolsSort();
        float[] Chances = new float[AllItems.pools.Length];
        for (int i = 0; i < AllItems.pools.Length; i++)
        {
            if(waveNum < AllItems.pools[i].MinWave) { Chances[i] = 0;continue; }
            Chances[i] = Mathf.Min((AllItems.pools[i].BaseChance 
                + AllItems.pools[i].GrowPerWave * (waveNum - AllItems.pools[i].MinWave))
                * (1 + Mathf.Max(GameStats.Luck, -99) / 100f), AllItems.pools[i].MaxChance);
            
        }
        for (int i = 0; i < Items.Length; i++)
        {
            Panels[i].gameObject.SetActive(true);
            float D100 = UnityEngine.Random.Range(0f, 100f);
            for (int j = 0 ; j < AllItems.pools.Length; j++)
            {
                if (D100 <= Chances[j])
                {
                    Items[i] = AllItems.pools[j].Items[UnityEngine.Random.Range(0, AllItems.pools[j].Items.Length)];
                    Infs[i].text = Items[i].Info();
                    Prise[i].text = $"{Items[i].Cost}";
                    Sprite[i].sprite = Items[i].sprite;
                    Panels[i].sprite = AllItems.pools[j].BackGround;
                    break;
                }
                if (j == AllItems.pools.Length-1)
                {
                    Items[i] = AllItems.pools[j].Items[UnityEngine.Random.Range(0, AllItems.pools[j].Items.Length)];
                    Infs[i].text = Items[i].Info();
                    Prise[i].text = $"{Items[i].Cost}";
                    Sprite[i].sprite = Items[i].sprite;
                    Panels[i].sprite = AllItems.pools[j].BackGround;
                    break;
                }
            }
        }
    }
}