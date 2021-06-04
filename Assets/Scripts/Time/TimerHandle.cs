using UnityEngine;

public class TimerHandle : MonoBehaviour
{
    Timer timer;

    public TimerHandle(Timer _timer)
    {
        timer = _timer;
    }

    public Timer GetTimer()
    {
        return timer;
    }
}
