using UnityEngine;

public class MissedAttackState : IState
{
    float timeToWait;
    float currentWaitTime;

    public MissedAttackState(float _timeToWait)
    {
        timeToWait = _timeToWait;
        currentWaitTime = 0;
    }

    public void OnEnter()
    {
        currentWaitTime = 0;
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
        currentWaitTime += Time.deltaTime;
    }

    public bool WaitingFinished()
    {
        return currentWaitTime >= timeToWait;
    }
}