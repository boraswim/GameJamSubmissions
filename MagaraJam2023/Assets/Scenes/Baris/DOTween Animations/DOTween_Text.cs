using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DOTween_Text : DOTween_Animation
{
    public void TintRed(TMP_Text text)
    {
        text.DOColor(Color.red, 1f);
    }
    public void TintGreen(TMP_Text text)
    {
        text.DOColor(Color.green, 1f);
    }
}
