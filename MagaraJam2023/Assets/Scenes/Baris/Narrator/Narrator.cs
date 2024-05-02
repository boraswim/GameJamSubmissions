using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Narrator : MonoBehaviour
{
    public NarratorSpeech currentSpeech;
    public List<NarratorSpeech> playedSpeeches = new List<NarratorSpeech>();
    public TMP_Text textToDisplay;
    public AudioSource audioSource;
    public static Narrator instance;
    private Coroutine current;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public void StartSpeech(NarratorSpeech narratorSpeech)
    {
        if (playedSpeeches.Exists(x => x == narratorSpeech))
            return;
        if (current != null)
            StopCoroutine(current);
        current = StartCoroutine(DisplayText(narratorSpeech));
        if (narratorSpeech.clip != null)
            audioSource.clip = narratorSpeech.clip;
        audioSource.Stop();
        audioSource.Play();
        currentSpeech = narratorSpeech;
        playedSpeeches.Add(narratorSpeech);
    }
    public IEnumerator DisplayText(NarratorSpeech narratorSpeech)
    {
        yield return new WaitForSeconds(0.1f);
        textToDisplay.text = "";
        foreach (char character in narratorSpeech.narratorText.ToCharArray())
        {
            textToDisplay.text += character;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
        textToDisplay.DOColor(new Color(1, 1, 1, 0), 3f).SetUpdate(true);
    }
}
