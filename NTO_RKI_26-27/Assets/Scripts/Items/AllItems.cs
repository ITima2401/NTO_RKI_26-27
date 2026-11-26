using System;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "AllItems", menuName = "Scriptable Objects/Items/AllItems")]
public class AllItems : ScriptableObject
{
    public ItemPool[] pools;
    public void PoolsSort()
    {
        if (pools == null) return;
        pools = pools.Where(p => p != null).OrderBy(p => p.MaxChance).ToArray();
    }
}
