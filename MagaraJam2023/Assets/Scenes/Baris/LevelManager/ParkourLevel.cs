using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourLevel : LevelRoom
{
    public int lives = 3;
    public void RemoveLife() { lives -= 1; }
    public void AddLife() { lives += 1; }
    public bool finished = false;
    public override bool IsFailed()
    {
        return lives <= 0;
    }
    public override bool isFinished()
    {
        return finished;
    }
    public void FinishParkour()
    {
        finished = true;
    }
}
