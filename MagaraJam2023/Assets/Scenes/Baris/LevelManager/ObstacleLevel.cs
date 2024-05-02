using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLevel : LevelRoom
{
    public bool failed = false, finished = false;
    public override bool IsFailed()
    {
        return failed;
    }
    public override bool isFinished()
    {
        return finished;
    }
}
