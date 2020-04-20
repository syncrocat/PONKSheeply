public interface IChaseRangeHandler
{
    void OnEnterChaseRange(object sender, ChaseRangeArgs args);

    void OnExitChaseRange(object sender, ChaseRangeArgs args);
}
