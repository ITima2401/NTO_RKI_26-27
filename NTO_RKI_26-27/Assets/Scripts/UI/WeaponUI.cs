using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponUI : ItemUI
{
    public void Subtract()
    {
        if (item is WeaponInfo weapon)
        {
            var bus = GameBootstrap.Instance?.Bus;
            if (bus == null) { Debug.Log("GameBootstrap = null"); return; }
            bus.Publish<WeaponSubtractEvent>(new WeaponSubtractEvent(weapon));
        }
    }
}
