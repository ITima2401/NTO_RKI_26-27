using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MiliWeapon : Weapon
{
    private List<GameObject> enemis = new List<GameObject>();
    private bool isRecharge;
    private bool isAttack;
    protected override void Update()
    {
        if(target == null)
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
                if((enemis[i].transform.position - transform.position).magnitude < minMagnitude)
                {
                    nearEnemy = enemis[i].transform;
                    minMagnitude = (enemis[i].transform.position - transform.position).magnitude;
                }
            }
            if (nearEnemy == null|| nearEnemy == Target) return; 
            target = nearEnemy;
            isAttack = true;
            Speed = BaseSpeed*3;
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
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemis.Remove(other.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && isAttack)
        {
            collision.gameObject.GetComponent<Enemy>().ChangeHP(Convert.ToInt32(-Damage * PlayerStats.Damage));
            isAttack = false;
            target = Target;
            Speed = BaseSpeed;
        }
    }
}
