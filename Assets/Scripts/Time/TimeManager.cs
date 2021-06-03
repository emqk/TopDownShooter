using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    List<Timer> timers = new List<Timer>();

    public static TimeManager instance;

    private void Awake()
    {
        Debug.Log("Time Manager created!");
        instance = this;
    }

    void Update()
    {
        // Update timers
        foreach (Timer timer in timers)
        {
            timer.UpdateMe();
        }

        // Remove finished timers
        for (int i = timers.Count - 1; i >= 0; i--)
        {
            if (timers[i].IsFinished)
            {
                timers.RemoveAt(i);
            }
        }
    }

    public void CreateTimer(float waitTime, UnityAction action)
    {
        Timer newTimer = new Timer(waitTime, action);
        timers.Add(newTimer);
    }
}
