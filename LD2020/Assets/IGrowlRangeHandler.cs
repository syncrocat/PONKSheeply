public interface IGrowlRangeHandler
{
    void OnEnterGrowlRange(object sender, GrowlRangeArgs args);

    void OnExitGrowlRange(object sender, GrowlRangeArgs args);
}