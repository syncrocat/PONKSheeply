using System;
using UnityEngine;

public class ShepherdMovementManager : MonoBehaviour, ITouchFloorListener
{
    public const int MaxMagnitude = 30;
    public const int JumpForce = 4000;
    public const int MoveForce = 200;
    public EventHandler<KickEventArgs> OnKickBall;
    public ShepherdCollisionManager _SCM;
    private bool _isJumping;
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        
        float moveForce = MoveForce * Time.deltaTime;
        var influenceVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            influenceVector += Vector3.left;
        }            
        if (Input.GetKey(KeyCode.RightArrow))
        {
            influenceVector += Vector3.right;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            influenceVector += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            influenceVector += Vector3.back;
        }

        influenceVector = Vector3.Normalize(influenceVector) * moveForce;
        if (_rb.velocity != Vector3.zero) transform.rotation = Quaternion.LookRotation(new Vector3(_rb.velocity.x, 0, _rb.velocity.z));

        if (influenceVector.x == 0 && influenceVector.y == 0 && influenceVector.z == 0 && _SCM.countCollides != 0)
        {
            float groundFriction = 1f;
            float newMag = _rb.velocity.magnitude - groundFriction;
            if (newMag < groundFriction) newMag = 0;
            _rb.velocity = Vector3.Normalize(_rb.velocity) * newMag;
        }

        var limitedVelocity = LimitProposedVelocity(_rb, influenceVector);
        _rb.velocity = new Vector3(limitedVelocity.x, _rb.velocity.y, limitedVelocity.z);

        if (Input.GetKey(KeyCode.Z))
        {
            if (!_isJumping)
            {
                _isJumping = true;
                // Set to 0 first pls
                _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
                _rb.AddForce(Vector3.up * JumpForce);
            }
        }
        if (Input.GetKey(KeyCode.X))
        {
            var posX = _rb.position.x;
            var posY = _rb.position.y;
            var posZ = _rb.position.z;
            OnKickBall?.Invoke(this, new KickEventArgs(posX, posY, posZ));
        }
    }

    public void OnTouchFloor(object sender, EventArgs args)
    {
        _isJumping = false;
    }

    public void OnLeaveFloor(object sender, EventArgs args)
    {
        // _isJumping = true; // Maybe implement this
    }

    private static Vector3 LimitProposedVelocity(Rigidbody rb, Vector3 influenceVector)
    {
        var existingVector = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        var newVector = existingVector + influenceVector;
        var newMagnitude = newVector.magnitude;

        // Check that new magnitude is within bounds
        var existingMagnitude = existingVector.magnitude;
        var magnitudeLimit = Math.Max(MaxMagnitude, existingMagnitude);
        if (newMagnitude > magnitudeLimit)
        {
            newVector = Vector3.Normalize(newVector) * magnitudeLimit;
        }

        return newVector;
    }
}
