using System;

public interface ITouchFloorListener
{
    void OnTouchFloor(object sender, EventArgs args);

    void OnLeaveFloor(object sender, EventArgs args);
}