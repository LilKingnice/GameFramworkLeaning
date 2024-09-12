using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicManager : BaseSingletonWithoutMono<MusicManager>
{
    private MusicManager()
    {
    }

    private AudioSource BGM = null;
    private float BGM_Volume = 0.5f;

    List<AudioSource> sfxList = new List<AudioSource>();
    //AudioSource sfx = null;
    GameObject sfxManager = null;
    float SFX_Volume = 0.5f;

    bool sfxIsPlaying=false;

    /// <summary>
    /// synchronous initialization value and slider value
    /// </summary>
    /// <param name="value"></param>
    public void SoundInit(ref float bgmvalue, ref float sfxvalue)
    {
        BaseMono.Instance.AddFixedUpdateListener(UpdateSFXList);
        bgmvalue = BGM_Volume;
        sfxvalue = SFX_Volume;
    }

    void UpdateSFXList()
    {
        if (sfxIsPlaying)
            return;
        //Go through the groups in reverse order, because inserting and deleting once will result in an error
        for (int i = sfxList.Count - 1; i >= 0; --i)
        {
            if (!sfxList[i].isPlaying && !sfxList[i].loop)
            {
                GameObject.Destroy(sfxList[i]);
                sfxList.RemoveAt(i);
            }
        }
    }


    /// <summary>
    /// Start
    /// </summary>
    /// <param name="musicName"></param>
    /// <param name="isloop"></param>
    /// <param name="isSync"></param>
    /// <param name="isDebuging">ABpackage Loading or Editor Loading</param>
    public void StartPlayMusic(string musicName, bool isloop = true, bool isSync = false, bool isDebuging = true)
    {
        if (BGM == null)
        {
            GameObject soundObj = new GameObject("BGM");
            GameObject.DontDestroyOnLoad(soundObj);
            BGM = soundObj.AddComponent<AudioSource>();
        }
        ResLoadingManager.Instance.LoadRES<AudioClip>((adio) =>
        {
            BGM.clip = adio;

            BGM.loop = isloop;

            BGM.volume = BGM_Volume;
            BGM.Play();
        }, musicName, "SFX", isSync, isDebuging);
    }

    //Pause
    public void PausePlayMusic()
    {
        if (BGM == null)
            return;
        else
        {
            if (BGM.isPlaying)
                BGM.Pause();
            else
                BGM.UnPause();
        }
    }

    //Restart
    public void RestartPlayMusic()
    {
        if (BGM == null)
            return;
        else
        {
            BGM.Stop();
            BGM.Play();
        }
    }

    //Slider ChangeValue synchronization
    public void ChangePlayMusicVolume(float v)
    {
        BGM_Volume = v;
        if (BGM == null)
            return;
        BGM.volume = BGM_Volume;
    }

    public void TrunOn()
    {
        //TODO Need set a toggle to control the sound volume to 1(on) of bgm or sfx
    }

    public void TrunOFF()
    {
        //TODO Need set a toggle to control the sound volume to 0(off) of bgm or sfx
    }





    /// <summary>
    /// PlaySFX
    /// </summary>
    /// <param name="name">fileName</param>
    /// <param name="callBack">callBack function</param>
    /// <param name="isloop">is loop or not</param>
    /// <param name="sync">is sync or not</param>
    public void StartPlaySFX(string sfxname, UnityAction<AudioSource> callBack, float slider_volume, bool sync = false, bool isDebuging = true, bool isloop = false)
    {
        sfxIsPlaying=true;
        if (sfxManager == null)
        {
            sfxManager = new GameObject("SFX");
        }
        ResLoadingManager.Instance.LoadRES<AudioClip>((adio) =>
        {
            AudioSource source = sfxManager.AddComponent<AudioSource>();
            source.clip = adio;
            source.loop = isloop;

            source.volume = slider_volume;
            source.Play();
            //source.PlayOneShot(adio);

            sfxList.Add(source);

            callBack?.Invoke(source);//usually be used in loop sfx
        }, sfxname, "SFX", sync, isDebuging);
    }

    public void PauseSFX(AudioSource sfx)
    {
        if (sfxList.Contains(sfx))
        {
            if (!sfx.loop)
            {
                sfx.Stop();
                sfxList.Remove(sfx);
                GameObject.Destroy(sfx);
            }
            else
            {
                if (sfx.isPlaying)
                    sfx.Pause();
                else
                    sfx.UnPause();
            }
        }
    }


    public void StopAllSFX()
    {
        for (int i = 0; i < sfxList.Count; i++)
        {
            if (sfxList[i].loop == true)
            {
                if (sfxList[i].isPlaying)
                    sfxList[i].Pause();
                else
                    sfxList[i].UnPause();
            }
            else
            {
                sfxIsPlaying=false;
                sfxList[i].Stop();
                // sfxList.Remove(sfxList[i]);
                // GameObject.Destroy(sfxList[i]);
            }
        }
    }

    public void ChangeSFXVolume(float value)
    {
        SFX_Volume = value;

        for (int i = 0; i < sfxList.Count; i++)
        {
            sfxList[i].volume = SFX_Volume;
        }
    }
}
