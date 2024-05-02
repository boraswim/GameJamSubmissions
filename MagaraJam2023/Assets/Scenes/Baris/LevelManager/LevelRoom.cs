using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class LevelRoom : MonoBehaviour
{
    public abstract bool IsFailed();
    public abstract bool isFinished();
    public UnityEvent onFinish;
    public UnityEvent onFail;
    public void Teleport(Transform target)
    {
        LevelManager.instance.player.transform.position = target.transform.position;
    }
    public void StartCheck()
    {
        StartCoroutine("WaitForFail");
        StartCoroutine("WaitForFinish");
    }
    public void StopCheck()
    {
        StopCoroutine("WaitForFail");
        StopCoroutine("WaitForFinish");
    }
    private IEnumerator WaitForFail()
    {
        if (LevelManager.instance.failedRooms.Exists(x => x == this))
            yield break;
        yield return new WaitUntil(() => IsFailed() == true);
        onFail.Invoke();
        StopCoroutine("WaitForFinish");
        LevelManager.instance.failedRooms.Add(this);
    }
    private IEnumerator WaitForFinish()
    {
        if (LevelManager.instance.finishedRooms.Exists(x => x == this))
            yield break;
        yield return new WaitUntil(() => isFinished() == true);
        onFinish.Invoke();
        StopCoroutine("WaitForFail");
        LevelManager.instance.finishedRooms.Add(this);
    }

    public void DebugTestMessage(string message)
    {
        Debug.Log(message);
    }
}
public enum LevelType
{
    Parkour, Labyrinth, WaveDefence, Fight
}