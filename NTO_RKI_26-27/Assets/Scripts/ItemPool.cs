using UnityEngine;

[CreateAssetMenu(fileName = "ItemPool", menuName = "Scriptable Objects/Items/ItemPool")]
public class ItemPool : ScriptableObject
{
    public int MinWave;
    public int MaxChance;
    public float BaseChance;
    public float GrowPerWave;
    public Sprite BackGround;
    public Item[] Items;
}
