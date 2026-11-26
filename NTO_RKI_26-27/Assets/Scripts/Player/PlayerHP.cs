using UnityEngine;

public class PlayerHP : MonoBehaviour, IDamage
{
    public int HP;
    public int MaxHP;
    private void OnEnable()
    {

        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) { Debug.LogError("GameBootstrap = null"); return; }
        bus.Subscribe<HPChangedEvent>(HPChange);
    }
    private void OnDisable()
    {
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) return;
        bus.Unsubscribe<HPChangedEvent>(HPChange);
    }
    public void TakeDamage(int damage)
    {
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) { Debug.LogError("GameBootstrap = null"); return; }
        bus.Publish<HPAddEvent>(new HPAddEvent(-damage,0));
    }
    private void HPChange(HPChangedEvent changedEvent)
    {
        HP = changedEvent.HP;
        MaxHP = changedEvent.MaxHP;
        if (HP <= 0) Death();
    }
    private void Death()
    {
        Debug.Log("Смерть");
    }
}
