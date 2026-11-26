using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class MiliWeapon : Weapon
{
    private List<GameObject> enemis = new List<GameObject>();
    private SphereCollider SphereCollider;
    private float baseRange;
    private bool isRecharge;
    private void Start()
    {
        SphereCollider = GetComponent<SphereCollider>();
        baseRange = SphereCollider.radius;
        SphereCollider.radius = Range;
        SphereCollider.isTrigger = true;
    }
    protected override void Update()
    {
        if (target == null)
        {
            target = Target;
            isAttack = false;
            isRecharge = false;
            Speed = BaseSpeed;
        }
        if (isChase && (target.position - transform.position).magnitude > 0.1f)
        {
            Vector3 dir = (target.position - transform.position).normalized;
            transform.Translate(dir * Speed * Time.deltaTime);
        }
        if (!isRecharge && !isAttack)
        {
            for (int i = enemis.Count - 1; i >= 0; i--)
                if (enemis[i] == null) enemis.RemoveAt(i);

            Transform nearEnemy = Target;
            float minMagnitude = math.INFINITY;
            for (int i = 0; i < enemis.Count; i++)
            {
                if ((enemis[i].transform.position - transform.position).magnitude < minMagnitude)
                {
                    nearEnemy = enemis[i].transform;
                    minMagnitude = (enemis[i].transform.position - transform.position).magnitude;
                }
            }
            if (nearEnemy == null || nearEnemy == Target) return;
            target = nearEnemy;
            isAttack = true;
            Speed = BaseSpeed * 3;
            SphereCollider.radius = baseRange;
            StartCoroutine(Recharging());
        }
    }
    IEnumerator Recharging()
    {
        isRecharge = true;
        yield return new WaitForSeconds(Recharge);
        isRecharge = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemis.Add(other.gameObject);
            if (isAttack && other.TryGetComponent<IDamage>(out var enemy))
            {
                enemy.TakeDamage(Convert.ToInt32(GameStats.MiliDamage * Damage * GameStats.Damage));
                isAttack = false;
                target = Target;
                Speed = BaseSpeed;
                SphereCollider.radius = Range;
                return;
            }
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && isAttack && other.TryGetComponent<IDamage>(out var enemy))
        {
            enemy.TakeDamage(Convert.ToInt32(GameStats.MiliDamage * Damage * GameStats.Damage));
            isAttack = false;
            target = Target;
            Speed = BaseSpeed;
            SphereCollider.radius = Range;
            return;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemis.Remove(other.gameObject);
        }
    }
}
