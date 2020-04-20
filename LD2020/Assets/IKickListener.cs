using System;

public interface IKickListener
{
    void OnEnteredKickRange(object sender, EventArgs args);

    void OnExitedKickRange(object sender, EventArgs args);
}
