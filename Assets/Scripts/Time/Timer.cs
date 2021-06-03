using UnityEngine;
using UnityEngine.Events;

public class Timer
{
    readonly float targetTime;
    float currentTime = 0;
    UnityAction action;

    public bool IsFinished { get => isFinished; }
    bool isFinished = false;

    public Timer(float _targetTime, UnityAction _action)
    {
        targetTime = _targetTime;
        action = _action;
    }

    public void UpdateMe()
    {
        if (isFinished)
            return;

        currentTime += Time.deltaTime;
        if (currentTime >= targetTime)
        {
            action.Invoke();
            isFinished = true;
        }
    }
}
