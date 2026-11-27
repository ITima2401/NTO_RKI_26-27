using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerHP))]
[RequireComponent(typeof(WeaponsPos))]
[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    public float Speed;
    Vector3 target;
    Rigidbody rb;
    private void Awake()
    {
        GameStats.Player = this.gameObject;
        rb = GetComponent<Rigidbody>();
        target = transform.position;
    }

    private void Update()
    {
        var mouse = Mouse.current;
        if (mouse != null)
        {
            if (mouse.leftButton.isPressed)
            {
                int layerMask = 1 << 8;
                Ray ray = Camera.main.ScreenPointToRay(mouse.position.ReadValue());

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
                {
                    target = hit.point;
                }
            }
        }
        if((target - transform.position).magnitude > 0.3f)
        {
            Vector3 dir = (target - transform.position).normalized;
            rb.linearVelocity = dir * Speed;

        }
        else rb.linearVelocity = Vector3.zero;
    }
    private void OnEnable()
    {
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) { Debug.LogError("GameBootstrap = null"); return; }
        bus.Subscribe<SpeedChangedEvent>(SpeedChange);
    }
    private void OnDisable()
    {
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) return;
        bus.Unsubscribe<SpeedChangedEvent>(SpeedChange);
    }
    private void SpeedChange(SpeedChangedEvent speed)
    {
        Speed = speed.Speed;
        Debug.Log(Speed);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Loot"))
        {
            var bus = GameBootstrap.Instance?.Bus;
            if (bus == null) { Debug.LogError("GameBootstrap = null"); return; }
            bus.Publish<MoneyAddEvent>(new MoneyAddEvent(5));//Доделать
            Destroy(other.gameObject);
        }
    }

}
