using System;
using UnityEngine;

public class GrowlRangeArgs : EventArgs
{
    public GameObject gameObject;

    public GrowlRangeArgs(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }
}