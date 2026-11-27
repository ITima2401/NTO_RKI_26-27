using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class RangeWeapon : Weapon
{
    private List<GameObject> enemis = new List<GameObject>();
    private SphereCollider SphereCollider;
    public GameObject Bullet;
    public float BulletRange;
    public float BulletSpeed;
    private bool isRecharge;
    void Start()
    {
        SphereCollider = GetComponent<SphereCollider>();
        SphereCollider.radius = Range;
        SphereCollider.isTrigger = true;
    }
    private void OnEnable()
    {
        StartCoroutine(Attack());
        isRecharge = true;
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemis.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemis.Remove(other.gameObject);
        }
    }
    public override void InstData(WeaponInfo weapon,Transform _target)
    {
        base.InstData(weapon,_target);
        Bullet = weapon.Bullet;
        BulletRange = weapon.BulletRange;
        BulletSpeed = weapon.BulletSpeed;
    }
    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if (isRecharge && enemis.Count != 0)
            {
                for (int i = enemis.Count - 1; i >= 0; i--)
                    if (enemis[i] == null) enemis.RemoveAt(i);
                if(enemis.Count == 0) continue;
                isRecharge = false;
                Vector3 dir = Vector3.zero;
                float minMagnitude = math.INFINITY;
                for (int i = 0; i < enemis.Count; i++)
                {
                    if ((enemis[i].transform.position - transform.position).magnitude < minMagnitude)
                    {
                        dir = (enemis[i].transform.position - transform.position).normalized;
                        minMagnitude = (enemis[i].transform.position - transform.position).magnitude;
                    }
                }
                if (Bullet == null) yield break;
                GameObject _bullet = Instantiate(Bullet, transform.position + dir, Quaternion.LookRotation(dir));
                _bullet.GetComponent<Bullet>().Damage = 
                    Convert.ToInt32(GameStats.RangeDamage * Damage * GameStats.Damage / 100);
                _bullet.GetComponent<Bullet>().dir = dir * BulletSpeed;
                _bullet.GetComponent<Bullet>()._tag = "Enemy";
                Destroy(_bullet, BulletRange);

                yield return new WaitForSeconds(Recharge);
                isRecharge = true;
            }
        }
    }
}
