using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour, IDamage
{
    protected Rigidbody rb;
    public Transform Player;
    public GameObject Loot;
    public int HP;
    public int MaxHP;
    public float Speed;
    public int Damage;
    public float SlowRange;
    public float StopRange;
    public float attackCD;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Player = GameStats.Player.transform;
    }
    public virtual void DataUpdate(EnemyData data)
    {
        HP = data.HP;
        MaxHP = data.HP;
        Speed = data.Speed;
        Damage = data.Damage;
        StopRange = data.StopRange;
        Loot = data.LootPref;
    }
    public void TakeDamage(int damage)
    {
        Debug.Log(-damage);
        ChangeHP(-damage);
    }
    public void ChangeHP(int hp)
    {
        HP += hp;
        if (HP > MaxHP ) HP = MaxHP;
        if ( HP <= 0 ) Death();
    }
    private void Death()
    {
        Instantiate( Loot,transform.position,Quaternion.identity );
        Destroy( this.gameObject );
    }
}
