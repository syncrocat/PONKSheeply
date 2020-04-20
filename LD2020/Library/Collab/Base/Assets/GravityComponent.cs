using System;
using UnityEngine;

public class GravityComponent : MonoBehaviour
{
    public int Gravity = 5000;
    private PhysicMaterial _material;
    private float _originalBounciness;
    private bool _isBouncing;

    Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _isBouncing = true;
        _material = GetComponent<Collider>().material;
        _originalBounciness = _material.bounciness;
        _rb = GetComponent<Rigidbody>();
    }

    private void ApplyGravity(Rigidbody rb)
    {
        if (_isBouncing)
        {
            float gravityForce = Gravity * Time.fixedDeltaTime;
            rb.AddForce(Vector3.down * gravityForce * rb.mass);
        } 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CompareTag("sheep") && _rb.velocity.magnitude >= 0.2)
        {
            _isBouncing = true;
            _material.bounciness = _originalBounciness;
        }

        ApplyGravity(_rb);
    }

    public void StopBounce()
    {
        Debug.Log("Got here");
        if (_rb.velocity.magnitude < 0.2)
        {
            _rb.velocity = Vector3.zero;
            _isBouncing = false;
            _material.bounciness = 0;
        }
    }
}
