using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]public GameObject PausePnl;
    public GameObject UyariPnl;
    public GameObject MenuMainPnl;
    public Button[] PauseButtons;

    private void Update()
    {
 
        if (!MenuMainPnl.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (Time.timeScale == 0f)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        PausePnl.SetActive(true);
        Time.timeScale = 0f;
    }
    public void UyariPanel(int number)
    {
        if (number == 1)
        {
            StartCoroutine(ButtonAnimation("false"));
            UyariPnl.SetActive(true);

        }
        else
        {
            StartCoroutine(ButtonAnimation("Open"));
            UyariPnl.SetActive(false);
        }

    }
    IEnumerator ButtonAnimation(string durum)
    {
        if(durum == "Open")
        {
            for (int i = 0; i < PauseButtons.Length; i++)
            {
                PauseButtons[i].GetComponent<RectTransform>().DOScale(1f, 0.5f).SetUpdate(true);
            }
            yield return new WaitForSeconds(0.2f);
        }else
        {
            for (int i = 0; i < PauseButtons.Length; i++)
            {
                PauseButtons[i].GetComponent<RectTransform>().DOScale(0f, 0.5f).SetUpdate(true);
            }
            yield return new WaitForSeconds(0.2f);
        }
        
    }
    public void Home()
    {
        Resume();
        MenuManager.Instance.MainMenuPnl.SetActive(true);
        MenuManager.Instance.IE();
        UyariPnl.SetActive(false);
        StartCoroutine(ButtonAnimation("Open"));
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        PausePnl.SetActive(false);

    }
}
