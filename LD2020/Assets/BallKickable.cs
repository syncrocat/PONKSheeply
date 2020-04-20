using System;
using UnityEngine;

public class BallKickable : MonoBehaviour, IKickListener
{

    public Material shinyMat;
    public Material normalMat;
    Rigidbody ballRB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ballRB = GetComponent<Rigidbody>();


    }

    public void OnEnteredKickRange(object sender, EventArgs args)
    {
        MeshRenderer mesh = gameObject.GetComponent<MeshRenderer>();
        mesh.material = shinyMat;

        if (transform.position.y < -1000) Destroy(this.gameObject);


    }

    public void OnExitedKickRange(object sender, EventArgs args)
    {
        MeshRenderer mesh = gameObject.GetComponent<MeshRenderer>();
        mesh.material = normalMat;
    }

    public void GetKicked (Vector3 kickForce)
    {
        ballRB.AddForce(kickForce);
    }
}
