public class MissedAttackState : IState
{
    float timeToWait;

    // Runtime data
    bool isWaitingFinished = false;
    TimerHandle timerHandle = null;

    public MissedAttackState(float _timeToWait)
    {
        timeToWait = _timeToWait;
    }

    public void OnEnter()
    {
        isWaitingFinished = false;
        timerHandle = TimeManager.instance.CreateTimer(timeToWait, FinishWaiting);
    }

    public void OnExit()
    {
        TimeManager.instance.RemoveTimerByHandle(timerHandle);
    }

    public void OnUpdate()
    {
    }

    public bool WaitingFinished()
    {
        return isWaitingFinished;
    }

    void FinishWaiting()
    {
        isWaitingFinished = true;
    }
}