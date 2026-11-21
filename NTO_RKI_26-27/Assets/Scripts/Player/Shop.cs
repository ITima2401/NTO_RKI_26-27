using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public AllItems AllItems;
    public TMP_Text[] Infs;
    public TMP_Text[] Prise;
    public Image[] Panels;
    public Image[] Sprite;
    public Item[] Items = new Item[3];
    private Spawner spawner;
    private void Awake()
    {
        spawner = GetComponent<Spawner>();
    }
    public void Buy(int buttonID)
    {
        if(PlayerStats.Money >= Items[buttonID].Cost)
        {
            PlayerStats.Money -= Items[buttonID].Cost;
            Panels[buttonID].gameObject.SetActive(false);
            PlayerStats.AddItem(Items[buttonID]);
        }
    }
    public void Refresh()
    {
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
    private void OnEnable()
    {
        Spawner.WaveEnd += Refresh;
    }
    private void OnDisable()
    {
        Spawner.WaveEnd -= Refresh;
    }
}
