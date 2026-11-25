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
    public void Show(WaveData _) => Menu.SetActive(true);
    private void OnEnable()
    {
        Spawner.WaveEnd += Refresh;
        WaveManager.WaveStart += Show;
    }
    private void OnDisable()
    {
        Spawner.WaveEnd -= Refresh;
        WaveManager.WaveStart -= Show;
    }
}
