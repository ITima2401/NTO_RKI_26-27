using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

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

    private void Awake()
    {
        target = Target;
        BaseChange(PlayerStats.Speed);
    }
    private void OnEnable()
    {
        PlayerStats.OnSpeedChanged += BaseChange;
    }
    private void OnDisable()
    {
        PlayerStats.OnSpeedChanged -= BaseChange;

    }
    public virtual void InstData(WeaponInfo weapon)
    {
        Range = weapon.Range;
        Damage = weapon.Damage;
        isChase = weapon.isChase;
        Recharge = weapon.Recharge;
    }
    private void BaseChange(float value)
    {
        BaseSpeed = value;
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
