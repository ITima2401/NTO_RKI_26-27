using System.Collections;
using UnityEngine;

public class RangeEnemy : Enemy
{
    public float BulletSpeed;
    public float AttackRange;
    public GameObject bullet;
    public bool isRecharge = true;
    void Update()
    {
        Vector3 dir = (Player.position - transform.position).normalized;
        float Magnitude = (Player.position - transform.position).magnitude;

        if (Magnitude - StopRange >= 0.2f)
        {
            rb.linearVelocity = dir * Speed;
        }
        else if(Magnitude - StopRange < 0f)
        {
            rb.linearVelocity = -dir * Speed;
        }
        else
        {
            if(isRecharge)StartCoroutine(Attack(dir));
            rb.linearVelocity = Vector3.zero;
        }
    }
    public override void DataUpdate(EnemyData data)
    {
        base.DataUpdate(data);
        attackCD = data.AttackCD;
        AttackRange = data.AttackRange;
        BulletSpeed = data.BulletSpeed;
        bullet = data.Bullet;
        Loot = data.LootPref;
    }
    IEnumerator Attack(Vector3 dir)
    {
        isRecharge = false;
        GameObject _bullet = Instantiate(bullet, transform.position + dir , Quaternion.LookRotation(dir));
        _bullet.GetComponent<Bullet>().Damage = Damage;
        _bullet.GetComponent<Bullet>().dir = dir * BulletSpeed;
        _bullet.GetComponent<Bullet>()._tag = "Player";
        Destroy(_bullet, AttackRange);
        yield return new WaitForSeconds(attackCD);
        isRecharge = true;
    }

}
