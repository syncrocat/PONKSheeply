using System;
using UnityEngine;

public class GravityComponent : MonoBehaviour
{
    public int Gravity = 5000;
    Rigidbody _rb;
    public ShepherdCollisionManager _SCM;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void ApplyGravity(Rigidbody rb)
    {
        if (CompareTag("Player") && _SCM.countCollides > 0) return;
        float gravityForce = Gravity * Time.fixedDeltaTime;
        rb.AddForce(Vector3.down * gravityForce * rb.mass);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyGravity(_rb);
    }
}
