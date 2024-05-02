using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public List<string> playedObjectiveStrings = new List<string>();
    public TMP_Text objectiveText;
    public void PlayObjectiveString(string playString)
    {
        if (playedObjectiveStrings.Exists(x => x == playString))
            return;
        objectiveText.text = playString;
        DOVirtual.Color(Color.clear, Color.white, 1f, x => { objectiveText.color = x; });
        playedObjectiveStrings.Add(playString);
    }
    public void ResetObjectiveString()
    {
        objectiveText.text = "";
    }

}
