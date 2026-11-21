using UnityEngine;

public class Enemy : MonoBehaviour
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
    public EnemyData EnemyData;
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
