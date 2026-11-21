using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform Target;
    protected Transform target;
    public float Speed;
    public float Damage;
    public float Recharge;
    public bool isChase = true;

    private void Awake()
    {
        target = Target;
    }
    void Update()
    {
        if (isChase)
        {
            Vector3 dir = (target.position - transform.position).normalized;
            transform.Translate(dir * Speed * Time.deltaTime);
        }
    }
}
