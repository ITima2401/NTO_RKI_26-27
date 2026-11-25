using UnityEngine;

[CreateAssetMenu(fileName = "AllItems", menuName = "Scriptable Objects/Items/AllItems")]
public class AllItems : ScriptableObject
{
    public Sprite Lvl1_BackGround;
    public Item[] ItemsLvl1;
    public Sprite Lvl2_BackGround;
    public Item[] ItemsLvl2;
    public Sprite Lvl3_BackGround;
    public Item[] ItemsLvl3;
}
