using UnityEngine;

[CreateAssetMenu(menuName ="Enemy")]
public class EnemyData : ScriptableObject
{
    public int HP;
    public int Damage;
    public float Speed;
    public float SlowRange;
    public float StopRange;
}
