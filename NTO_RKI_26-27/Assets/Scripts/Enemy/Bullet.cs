using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public int Damage;
    public Vector3 dir;
    public Rigidbody rb;
    public string _tag;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<Collider>().isTrigger = true;
    }

    private void Update()
    {
        rb.linearVelocity = dir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_tag) && other.isTrigger == false)
        {
            PlayerStats.HP -= Damage;
            Destroy(gameObject);
        }
    }
}