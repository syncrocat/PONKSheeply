using System;
using System.Collections.Generic;
using UnityEngine;

public class ShepherdCollisionManager : MonoBehaviour
{
    public EventHandler OnTouchFloor;
    public EventHandler OnLeaveFloor;
    public int countCollides;

    public List<MonoBehaviour> toSubscribe;
    // Start is called before the first frame update
    void Start()
    {
        countCollides = 0;
        foreach (ITouchFloorListener item in toSubscribe)
        {
            OnTouchFloor += item.OnTouchFloor;
            OnLeaveFloor += item.OnLeaveFloor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("doggy_radar_component")) countCollides++;

        Debug.Log("Touching Floor");
        if (!other.gameObject.CompareTag("doggy_radar_component")) OnTouchFloor?.Invoke(this, EventArgs.Empty);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("doggy_radar_component")) countCollides--;

        OnLeaveFloor?.Invoke(this, EventArgs.Empty);
    }

}
