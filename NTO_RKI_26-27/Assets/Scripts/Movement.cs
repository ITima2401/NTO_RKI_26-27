using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    Vector3 target;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = transform.position;
        PlayerStats.Speed = 5;
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
            rb.linearVelocity = dir * PlayerStats.Speed;
            //transform.Translate(dir * PlayerStats.Speed * Time.deltaTime);

        }
    }
    

}
