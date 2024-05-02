using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [Serializable]
    public class SoundElement
    {
        public string name;
        public AudioClip clip;
        public float volumeScale;
    }
    public List<SoundElement> elements;
    public string startMusicString;

    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    public event Action<bool> OnMainMenuChange;
    public event Action<bool> OnSFXChange;

    private bool isMainMenuOn;
    private bool isSFXOn;
    private float mainMenuVolume;
    private float sfxVolume;

    public bool IsMainMenuOn
    {
        get
        {
            return isMainMenuOn;
        }
        set
        {
            isMainMenuOn = value;
            musicAudioSource.volume = value ? 1:0;
            OnMainMenuChange?.Invoke(isMainMenuOn);
        }
    }
    public bool IsSFXOn
    {
        get
        {
            return isSFXOn;
        }
        set
        {
            isSFXOn = value;
            OnSFXChange?.Invoke(isSFXOn);
        }
    }
    public float MainMenuVolume
    {
        get
        {
            return mainMenuVolume;
        }
        set
        {
            mainMenuVolume = value;
            musicAudioSource.volume = value;
        }
    }
    public float SFXVolume
    {
        get
        {
            return sfxVolume;
        }
        set
        {
            sfxVolume = value;
            sfxAudioSource.volume = value;
        }
    }

    private void Start()
    {
        IsSFXOn = true;
        IsMainMenuOn = true;
        PlayMainMenuMusic(startMusicString);

        mainMenuVolume = 1f;
        sfxVolume = 1f;
    }

    public void PlaySFXAudio(string clip)
    {
        if (IsSFXOn)
        {
            SoundElement _element = elements.Find(element => element.name == clip);
            sfxAudioSource.PlayOneShot(_element.clip, _element.volumeScale);
        }
    }

    public void PlayAudioAtPoint(string clip, Vector3 position)
    {
        if (IsSFXOn)
        {
            SoundElement _element = elements.Find(element => element.name == clip);
            AudioSource.PlayClipAtPoint(_element.clip, position);
        }
    }

    public void PlayMainMenuMusic(string clip)
    {
        if (isMainMenuOn)
        {
            SoundElement _element = elements.Find(element => element.name == clip);
            musicAudioSource.clip = _element.clip;
            musicAudioSource.volume = _element.volumeScale;
            musicAudioSource.Play();
        }
    }

    public AudioClip GetAudio(string clip)
    {
        return elements.Find(element => element.name == clip).clip;
    }
}
