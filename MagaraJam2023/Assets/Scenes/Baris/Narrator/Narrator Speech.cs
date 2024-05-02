using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NarratorSpeech : MonoBehaviour
{
    [TextArea(3, 10)]
    public string narratorText;
    public AudioClip clip;
    public void Play()
    {
        Narrator.instance.StartSpeech(this);
    }
}