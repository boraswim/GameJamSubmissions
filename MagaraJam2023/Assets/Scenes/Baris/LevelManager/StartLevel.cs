using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel : LevelRoom
{
    public bool touchedSword = false;
    public override bool IsFailed()
    {
        return false;
    }
    public override bool isFinished()
    {
        return touchedSword;
    }
    public void TouchSword()
    {
        touchedSword = true;
    }
}
