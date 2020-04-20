using System;
using System.Collections.Generic;
using UnityEngine;

public class GrowlRadar : MonoBehaviour
{
    public WolfBehaviour wolfBehaviourScript;
    public EventHandler<GrowlRangeArgs> OnEnterGrowlRange;
    public EventHandler<GrowlRangeArgs> OnExitGrowlRange;
    public List<GameObject> objectsToTrack;

    // Start is called before the first frame update
    void Start()
    {
        OnEnterGrowlRange += wolfBehaviourScript.OnEnterGrowlRange;
        OnExitGrowlRange += wolfBehaviourScript.OnExitGrowlRange;
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
            OnEnterGrowlRange?.Invoke(this, new GrowlRangeArgs(other.gameObject));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (objectsToTrack.Contains(other.gameObject))
        {
            OnExitGrowlRange?.Invoke(this, new GrowlRangeArgs(other.gameObject));
        }
    }
}
