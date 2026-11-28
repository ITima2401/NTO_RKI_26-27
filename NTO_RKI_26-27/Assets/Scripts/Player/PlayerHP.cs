using System.Collections;
using UnityEngine;

public class PlayerHP : MonoBehaviour, IDamage
{
    public int HP;
    public int MaxHP;
    [SerializeField] GameObject deadPanel;
    private void OnEnable()
    {
        MaxHP = GameStats.MaxHP;
        HP = GameStats.HP;
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) { Debug.LogError("GameBootstrap = null"); return; }
        bus.Subscribe<HPChangedEvent>(HPChange);
        bus.Subscribe<WaveStartedEvent>(WaveStart);
        bus.Subscribe<WaveEndedEvent>(WaveEnd);
    }
    private void OnDisable()
    {
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) return;
        bus.Unsubscribe<HPChangedEvent>(HPChange);
        bus.Unsubscribe<WaveEndedEvent>(WaveEnd);
        bus.Unsubscribe<WaveStartedEvent>(WaveStart);
    }
    private void WaveStart(WaveStartedEvent _) { StartCoroutine(Regen()); }
    private void WaveEnd(WaveEndedEvent _) { StopAllCoroutines(); }
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
        deadPanel.SetActive(true);
        Debug.Log("Смерть");
    }
    IEnumerator Regen()
    {

        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) { Debug.LogError("GameBootstrap = null"); yield break ; }
        while (true)
        {
            if(GameStats.Regen <= 0) break;
            yield return new WaitForSeconds(5 / GameStats.Regen);
            bus.Publish<HPAddEvent>(new HPAddEvent(1,0));
        }
    }
}
