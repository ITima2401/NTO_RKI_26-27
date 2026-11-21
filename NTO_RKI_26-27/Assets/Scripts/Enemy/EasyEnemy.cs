using UnityEngine;

public class EasyEnemy : Enemy
{
    private float slow;
    private float attackCD;
    private void Awake()
    {
        HP = EnemyData.HP;
        MaxHP = EnemyData.HP;
        Speed = EnemyData.Speed;
        Damage = EnemyData.Damage;
        SlowRange = EnemyData.SlowRange;
        StopRange = EnemyData.StopRange;

        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        Player = PlayerStats.Player.transform;
    }
    void Update()
    {
        float Magnitude = (Player.position - transform.position).magnitude;
        if ( Magnitude > SlowRange)
        {
            Vector3 dir = (Player.position - transform.position).normalized;
            rb.linearVelocity = dir * Speed;
        }
        else if(Magnitude > StopRange)
        {
            slow = Mathf.Max(Magnitude  / SlowRange,0.6f);
            Vector3 dir = (Player.position - transform.position).normalized;
            rb.linearVelocity = dir * Speed * slow;
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats.HP -= Damage;
        }
        
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            attackCD += Time.fixedDeltaTime;
            if (attackCD > 0.4f) 
            { 
                PlayerStats.HP -= Damage;
                attackCD = 0f;
            }
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            attackCD = 0;
        }
    }
}
