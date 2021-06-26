public class ExplosionAttackState : IState
{
    // Data
    BombAI owner;
    float timeToExplode = 1.25f;

    // Runtime data
    TimerHandle timerHandle = null;

    public ExplosionAttackState(BombAI _owner)
    {
        owner = _owner;
    }

    public void OnEnter()
    {
        owner.ShowSmoke();
        timerHandle = TimeManager.instance.CreateTimer(timeToExplode, owner.Explode);
    }

    public void OnExit()
    {
        TimeManager.instance.RemoveTimerByHandle(timerHandle);
    }

    public void OnUpdate()
    {
    }
}
