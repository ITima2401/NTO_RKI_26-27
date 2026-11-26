using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform Target;
    protected Transform target;
    public float Range;
    public float Speed;
    public float BaseSpeed;
    public float Damage;
    public float Recharge;
    public bool isAttack;
    public bool isChase = true;

    private void OnEnable()
    {
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) { Debug.LogError("GameBootstrap = null"); return; }
        bus.Subscribe<SpeedChangedEvent>(BaseChange);
    }
    private void OnDisable()
    {
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) return;
        bus.Unsubscribe<SpeedChangedEvent>(BaseChange);

    }
    public virtual void InstData(WeaponInfo weapon,Transform _target)
    {
        Target = _target;
        target = _target;
        Range = weapon.Range;
        Damage = weapon.Damage;
        isChase = weapon.isChase;
        Recharge = weapon.Recharge;
        BaseSpeed = GameStats.Speed;
        Speed = GameStats.Speed;
    }
    private void BaseChange(SpeedChangedEvent value)
    {
        BaseSpeed = value.Speed;
        if (isAttack) Speed = BaseSpeed * 3;
        else Speed = BaseSpeed;
    }
    protected virtual void Update()
    {
        if (isChase)
        {
            Vector3 dir = (target.position - transform.position).normalized;
            transform.Translate(dir * Speed * Time.deltaTime);
        }
    }
}
