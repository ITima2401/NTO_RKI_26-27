using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    public int HP;
    public int Damage;
    public float Speed;
    public float StopRange;
    public GameObject EnemyPref;
    public GameObject LootPref;
    [Header("Только для Easy Enemy")]
    public float SlowRange;
    [Header("Только для Range Enemy")]
    public float AttackCD;
    public float AttackRange;
    public float BulletSpeed;
    public GameObject Bullet;
}
