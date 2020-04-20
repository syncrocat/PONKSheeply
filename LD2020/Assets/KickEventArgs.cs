using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickEventArgs : EventArgs
{
    public readonly float originX;
    public readonly float originY;
    public readonly float originZ;

    public KickEventArgs(float originX, float originY, float originZ)
    {
        this.originX = originX;
        this.originY = originY;
        this.originZ = originZ;
    }
}
