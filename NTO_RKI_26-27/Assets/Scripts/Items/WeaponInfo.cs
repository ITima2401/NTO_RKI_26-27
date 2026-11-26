using UnityEngine;

[CreateAssetMenu(fileName = "WeaponInfo", menuName = "Scriptable Objects/Items/WeaponInfo")]
public class WeaponInfo : Item
{
    public GameObject WeaponPrefab;
    public float Damage;
    public float Recharge;
    public bool isChase = true;
    public float Range;
    [Header("Только для Range Weapon")]
    public GameObject Bullet;
    public float BulletRange;
    public float BulletSpeed;
}
