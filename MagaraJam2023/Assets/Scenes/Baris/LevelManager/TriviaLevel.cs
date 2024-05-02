using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriviaLevel : LevelRoom
{
    public int trueAnswerCount = 0, wrongAnswerCount = 0;
    public void AddWrongAnswer() { wrongAnswerCount += 1; }
    public void AddTrueAnswer() { trueAnswerCount += 1; }
    public override bool IsFailed()
    {
        return wrongAnswerCount >= 2;
    }
    public override bool isFinished()
    {
        return trueAnswerCount >= 2;
    }
}
