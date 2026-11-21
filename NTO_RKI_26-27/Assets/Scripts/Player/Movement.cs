using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    float Speed = 7;
    Vector3 target;
    Rigidbody rb;
    private void Awake()
    {
        PlayerStats.Player = this.gameObject;
        PlayerStats.Speed = Speed;
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
        if((target - transform.position).sqrMagnitude > 0.1f)
        {
            Vector3 dir = (target - transform.position).normalized;
            rb.linearVelocity = dir * Speed;

        }
    }
    private void OnEnable()
    {
        PlayerStats.OnSpeedChanged += SpeedChange;
    }
    private void OnDisable()
    {
        PlayerStats.OnSpeedChanged += SpeedChange;
    }
    private void SpeedChange(float speed)
    {
        Speed = speed;
    }

}
