using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Kicker : MonoBehaviour, IKickListener
{
    float kickCharge;
    float maxTick;
    float minTick;
    bool kickHeld;
    float kickScaleUp;
    private GameObject ball;
    LineRenderer line;
    float maxKickCharge;
    private MusicPlayer _musicPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _musicPlayer = GameObject.FindWithTag("musicPlayer").GetComponent<MusicPlayer>();
        kickCharge = 0;
        minTick = 5;
        maxKickCharge = 35;
        kickScaleUp = 12500;
        maxTick = 60;
        kickHeld = false;
        line = gameObject.GetComponent<LineRenderer>();
        ball = GameObject.FindWithTag("sheep");
    }

    void FixedUpdate()
    {
        if (kickHeld)
        {
            float adjustedChargeTick = Mathf.Lerp(maxTick, minTick, Mathf.Pow((kickCharge / maxKickCharge), 2));
            kickCharge += adjustedChargeTick * Time.fixedDeltaTime;
            // Debug.Log(kickCharge);
            if (kickCharge > maxKickCharge) kickCharge = maxKickCharge;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ball)
        {
            float distToFucker = 4f;

            Vector3 toBallVector = ball.transform.position - transform.position;
            Vector3 zz_ring_point = Vector3.Normalize(toBallVector) * distToFucker;
            zz_ring_point = transform.position + new Vector3(zz_ring_point.x, 0, zz_ring_point.z);

            Vector3 newVector = Vector3.Normalize(ball.transform.position - zz_ring_point);


            //Vector3 thing = Vector3.Normalize(ball.transform.position  - transform.position) * kickCharge;
            Vector3 thing = newVector * kickCharge;
            if (Input.GetKey(KeyCode.X))
            {
                kickHeld = true;
            }
            else
            {
                // Kick on release
                if (line.enabled && kickHeld)
                {
                    _musicPlayer.PlaySheepKickSound(UnityEngine.Random.Range(0, 5));
                    ball.GetComponent<BallKickable>().GetKicked(thing * kickScaleUp);
                }
                kickCharge = 0;
                kickHeld = false;
            }

            // Angle and length of indicator
            if (line.enabled)
            {
                Vector3 startLine = transform.position;
                Vector3 endLine = transform.position + thing;
                line.SetPositions(new Vector3[] { startLine, endLine });
            }


        }

        /*
  *LITERAL NONSENSE
  * Vector3 toBallVect = ball.transform.position - transform.position;
  float yaw = Mathf.Atan(toBallVect.x / (-toBallVect.y));
  Debug.Log("Yaw: " + yaw);
  float maxDistToBall = ball.GetComponent<SphereCollider>().radius + this.GetComponent<SphereCollider>().radius;
  Debug.Log("Max dist:" + maxDistToBall);
  Debug.Log("Current dist:" + toBallVect.magnitude);
  float p = toBallVect.magnitude / maxDistToBall;
  float pitch = Mathf.Lerp(0, Mathf.PI * 0.5f, p);
  Debug.Log("Pitch " + pitch);

        Vector3 newVector = new Vector3(Mathf.Cos(yaw) * Mathf.Cos(pitch), Mathf.Sin(pitch), Mathf.Sin(yaw) * Mathf.Cos(pitch)); */
    }

    public void OnEnteredKickRange(object sender, EventArgs args)
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.enabled = true;
    }

    public void OnExitedKickRange(object sender, EventArgs args)
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.enabled = false;
    }
}
