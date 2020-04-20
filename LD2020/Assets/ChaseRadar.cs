using System;
using System.Collections.Generic;
using UnityEngine;

public class ChaseRadar : MonoBehaviour
{
    public WolfBehaviour wolfBehaviourScript;
    public EventHandler<ChaseRangeArgs> OnEnterChaseRange;
    public EventHandler<ChaseRangeArgs> OnExitChaseRange;
    public List<GameObject> objectsToTrack;

    // Start is called before the first frame update
    void Start()
    {
        OnEnterChaseRange += wolfBehaviourScript.OnEnterChaseRange;
        OnExitChaseRange += wolfBehaviourScript.OnExitChaseRange;
        objectsToTrack = new List<GameObject>()
        {
            GameObject.FindWithTag("Player"),
            GameObject.FindWithTag("sheep"),
        };
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (objectsToTrack.Contains(other.gameObject))
        {
            OnEnterChaseRange?.Invoke(this, new ChaseRangeArgs(other.gameObject));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (objectsToTrack.Contains(other.gameObject))
        {
            OnExitChaseRange?.Invoke(this, new ChaseRangeArgs(other.gameObject));
        }
    }
}
