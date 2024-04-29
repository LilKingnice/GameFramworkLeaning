using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMusic : MonoBehaviour
{
    [Header("=========BGM SETTINGS=========")]
    [SerializeField] private Button PlayBGM;
    [SerializeField] private Button PauseBGM;
    [SerializeField] private Button RestartBGM;

    [SerializeField] private Slider BGM_slider;

    float bgmDefualtVolume;

    [Header("=========SFX SETTINGS=========")]
    [SerializeField] private Button Playsfx1;
    [SerializeField] private Button Playsfx2;
    [SerializeField] private Button Stopsfx;
    [SerializeField] private Button StopAllsfx;
    [SerializeField] private Slider SFX_slider;


    AudioSource currentSFX;
    float sfxDefualtVolume;

    void Start()
    {
        MusicManager.Instance.SoundInit(ref bgmDefualtVolume, ref sfxDefualtVolume);

        PlayBGM.onClick.AddListener(playBGM);
        PauseBGM.onClick.AddListener(pauseBGM);
        RestartBGM.onClick.AddListener(restartBGM);
        BGM_slider.onValueChanged.AddListener(bgm_slider);


        Playsfx1.onClick.AddListener(playSFX);
        Playsfx2.onClick.AddListener(playSFXLoop);

        Stopsfx.onClick.AddListener(pauseSFX);
        
        StopAllsfx.onClick.AddListener(pauseAllSFX);


        SFX_slider.onValueChanged.AddListener(sfx_slider);

        BGM_slider.value = bgmDefualtVolume;//change the initialization value
        bgm_slider(BGM_slider.value);//synchronous the initialization value

        SFX_slider.value = sfxDefualtVolume;
        sfx_slider(SFX_slider.value);
    }



    private void bgm_slider(float arg0)
    {
        // if(BGM_slider.value!=arg0)
        MusicManager.Instance.ChangePlayMusicVolume(arg0);
    }

    private void sfx_slider(float arg0)
    {
        // if(BGM_slider.value!=arg0)
        MusicManager.Instance.ChangeSFXVolume(arg0);
    }
    void playBGM()
    {
        MusicManager.Instance.StartPlayMusic("DIDIDI.mp3");
    }

    private void pauseBGM()
    {
        MusicManager.Instance.PausePlayMusic();
    }

    private void restartBGM()
    {
        MusicManager.Instance.RestartPlayMusic();
    }

    private void playSFX()
    {
        MusicManager.Instance.StartPlaySFX("Player Projectile Launch.ogg", (res) =>
        {
            currentSFX = res;
        }, SFX_slider.value, false, true, false);
    }
    private void playSFXLoop()
    {
        MusicManager.Instance.StartPlaySFX("木鱼.mp3", (res) =>
        {
            currentSFX = res;
        }, SFX_slider.value, false, true, true);
    }

    void pauseSFX()
    {
        MusicManager.Instance.PauseSFX(currentSFX);
    }

    void pauseAllSFX()
    {
        MusicManager.Instance.StopAllSFX();
    }

    /// <summary>
    /// OnGUI is called for rendering and handling GUI events.
    /// This function can be called multiple times per frame (one call per event).
    /// </summary>
    // void OnGUI()
    // {
    //     if(GUILayout.Button("播放背景音乐"))
    //     {
    //         MusicManager.Instance.StartPlayMusic("DIDIDI.mp3");
    //     }
    // }
}
