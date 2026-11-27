using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowRelic : MonoBehaviour
{
    private List<GameObject> Relics = new List<GameObject>();
    [SerializeField] GameObject relicGrid;
    [SerializeField] GameObject RelicPref;

    private List<GameObject> Weapons = new List<GameObject>();
    [SerializeField] GameObject weaponGrid;
    [SerializeField] GameObject WeaponPref;

    private void OnEnable()
    {
        var bus = GameBootstrap.Instance?.Bus;
        if(bus == null ) { Debug.LogError("GameBootstrap = 0"); return; }
        bus.Subscribe<ItemsChangedEvent>(RefreshItem);
    }
    private void OnDisable()
    {
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) { Debug.LogError("GameBootstrap = 0"); return; }
        bus.Unsubscribe<ItemsChangedEvent>(RefreshItem);
    }
    private void RefreshItem(ItemsChangedEvent items)
    {
        if (items.Item == null || WeaponPref == null || RelicPref == null
            || weaponGrid == null || relicGrid == null) return;
        if (Relics.Count > 0)
        {
            foreach(GameObject item in Relics) Destroy(item);
            Relics.Clear();
        }
        if (Weapons.Count > 0)
        {
            foreach (GameObject item in Weapons) Destroy(item);
            Weapons.Clear();
        }
        foreach (var item in items.Item)
        {
            if (item == null) continue;
            if (item is WeaponInfo)
            {
                GameObject image = Instantiate(WeaponPref, weaponGrid.transform);
                image.GetComponent<Image>().sprite = item.sprite;
                image.GetComponent<ItemUI>().item = item;
                Weapons.Add(image);
            }
            else
            {
                GameObject image = Instantiate(RelicPref, relicGrid.transform);
                image.GetComponent<Image>().sprite = item.sprite;
                image.GetComponent<ItemUI>().item = item;
                Relics.Add(image);
            }
        }
    }
}
