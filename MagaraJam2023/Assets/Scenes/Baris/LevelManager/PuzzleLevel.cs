using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleLevel : LevelRoom
{
    public int tryCount = 2;
    public string answerString = "";
    public UnityEvent onReset;
    public List<string> possibleAnswers = new List<string>();
    public override bool IsFailed()
    {
        return tryCount == 0;
    }
    public override bool isFinished()
    {
        return possibleAnswers.Exists(x => x == answerString);
    }
    public IEnumerator ResetAnswer()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        answerString = "";
        onReset.Invoke();
        tryCount -= 1;
    }
    public void ClickButton(int x)
    {
        answerString += x.ToString();
        if (answerString.Length >= 4 && !possibleAnswers.Exists(x => x == answerString))
            StartCoroutine("ResetAnswer");
    }
}
