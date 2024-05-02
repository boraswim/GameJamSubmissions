using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public GameObject SettingsPanel, MainMenuPnl, HealthBar, StaminaBar;
    public Button[] MainMenuBtns;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    void Start()
    {
        IE();
    }
    public void IE()
    {
        StartCoroutine("MainMenuCoroutine");
    }
    public IEnumerator MainMenuCoroutine()
    {
        for (int i = 0; i < MainMenuBtns.Length; i++)
            MainMenuBtns[i].GetComponent<RectTransform>().DOScale(0f, 0f);
        for (int i = 0; i < MainMenuBtns.Length; i++)
        {
            MainMenuBtns[i].GetComponent<RectTransform>().DOScale(1f, 0.5f)
            .SetEase(Ease.OutBack);
            yield return new WaitForSeconds(0.3f);
        }
    }
    public void SettingsBtn(bool state)
    {
        SettingsPanel.SetActive(state);
    }
    public void CloseMenu()
    {
        MainMenuPnl.SetActive(false);
        for (int i = 0; i < MainMenuBtns.Length; i++)
            MainMenuBtns[i].GetComponent<RectTransform>().DOScale(0f, 0f);
    }
    public void OpenBars()
    {
        HealthBar.GetComponent<RectTransform>().DOScale(1f, 1f);
    }
}