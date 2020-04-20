using System;
using UnityEngine;

public class KickWatcher : MonoBehaviour, IKickListener
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnteredKickRange(object sender, EventArgs args)
    {
        Transform t = GetComponent<Transform>();
        t.localScale *= 2;
    }

    public void OnExitedKickRange(object sender, EventArgs args)
    {
        Transform t = GetComponent<Transform>();
        t.localScale /= 2;
    }
}
