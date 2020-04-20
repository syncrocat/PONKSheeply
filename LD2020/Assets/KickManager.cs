using System;
using System.Collections.Generic;
using UnityEngine;

public class KickManager : MonoBehaviour
{
    public event EventHandler OnEnteredKickRange;
    public event EventHandler OnExitedKickRange;

    // Start is called before the first frame update
    void Start()
    {
        var toSubscribe = new List<MonoBehaviour>();

        var sheep = GameObject.FindWithTag("sheep");
        toSubscribe.Add(sheep.GetComponent<BallKickable>());
        toSubscribe.Add(this.GetComponentInParent<Kicker>());

        foreach (IKickListener item in toSubscribe)
        {
            OnEnteredKickRange += item.OnEnteredKickRange;
            OnExitedKickRange += item.OnExitedKickRange;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "sheep")
        {
            OnEnteredKickRange?.Invoke(this, EventArgs.Empty);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "sheep")
        {
            OnExitedKickRange?.Invoke(this, EventArgs.Empty);
        }
    }
}
