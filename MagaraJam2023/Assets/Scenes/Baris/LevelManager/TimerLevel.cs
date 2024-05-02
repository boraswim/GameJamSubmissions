using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerLevel : LevelRoom
{
    public float timeLeft;
    public bool started = false, finished = false;
    public override bool IsFailed()
    {
        return timeLeft <= 0;
    }
    public override bool isFinished()
    {
        return finished;
    }
    public void StartTimer()
    {
        started = true;
    }
    public void StopTimer()
    {
        started = false;
    }
    public void FinishLevel()
    {
        finished = true;
    }
    void Update()
    {
        if (timeLeft > 0 && started && !finished)
            timeLeft -= Time.deltaTime;
    }
}
