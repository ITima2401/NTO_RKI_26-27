using UnityEngine;

public class Item : ScriptableObject
{
    public int Cost;
    public Sprite sprite;
    public virtual string Info()
    {
        return "";
    }
    public virtual bool isLegal()
    {
        return GameStats.Money >= Cost;
    }
}
