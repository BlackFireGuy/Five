using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MusicMgr : BaseSingleton<MusicMgr>
{
    //唯一的背景音乐组件
    private AudioSource bkMusic = null;
    //音乐大小
    private float bkValue = 1f;
    //音效大小
    private float soundValue = 1f;
    //音效依附对象
    private GameObject soundObj = null;
    //音效列表
    private List<AudioSource> soundList = new List<AudioSource>();

    public MusicMgr()
    {
        MonoMgr.GetInstance().AddUpdateListener(Update);
    }

    void Update()
    {
        for (int i = soundList.Count - 1; i >= 0; i--)
        {
            if (!soundList[i].isPlaying)
            {
                GameObject.Destroy(soundList[i]);
                soundList.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="name"></param>
    public void PlayBMusic(string name)
    {
        if (bkMusic == null)
        {
            GameObject obj = new GameObject("BkMusic");
            bkMusic = obj.AddComponent<AudioSource>();
        }
        //异步加载背景音乐、加载完成后播放
        /*ResMgr.GetInstance().LoadAsync<AudioClip>(name, (obj) => {
            bkMusic.clip = obj;
            bkMusic.loop = true;
            bkMusic.volume = bkValue;
            bkMusic.Play();
        });*/
        ResMgr.GetInstance().LoadAssetsAsync<AudioClip>(name, (objs)=>
        {
            switch (objs.Status)
            {
                case AsyncOperationStatus.Succeeded:
                    AudioClip loadedClip = objs.Result[0];
                    //异步加载远程下载下来的背景音乐
                    //加载完成后播放
                    bkMusic.clip = loadedClip;
                    bkMusic.loop = true;
                    bkMusic.volume = bkValue;
                    bkMusic.Play();
                    break;
                default:
                    break;
            }

        });
    }
    /// <summary>
    /// 改变背景音乐音量大小
    /// </summary>
    /// <param name="v"></param>
    public void ChangeBKValue(float v)
    {
        bkValue = v;
        if (bkMusic == null)
            return;
        bkMusic.volume = bkValue;
    }

    /// <summary>
    /// 暂停背景音乐
    /// </summary>
    public void PauseBKMusic()
    {
        if (bkMusic == null)
            return;
        bkMusic.Pause();
    }

    /// <summary>
    /// 停止背景音乐
    /// </summary>
    public void StopBkMusic()
    {
        if (bkMusic == null)
            return;
        bkMusic.Stop();
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="name"></param>
    public void PlaySound(string name,bool isloop = false,UnityAction<AudioSource> callback = null )
    {
        if(soundObj == null)
        {
            soundObj = new GameObject("Sound");
        }
      
        //当音效资源异步加载结束后，再添加一个音效
        /*ResMgr.GetInstance().LoadAsync<AudioClip>(PathCfg.PATH_MUSIC_SOUND + name, (clip) => {
            AudioSource source = soundObj.AddComponent<AudioSource>();
            source.clip = clip;
            source.loop = isloop;
            source.volume = soundValue;
            source.Play();
            soundList.Add(source);
            if (callback != null)
                callback(source);

        });*/
        ResMgr.GetInstance().LoadAssetsAsync<AudioClip>(name, (objs)=>
        {
            switch (objs.Status)
            {
                case AsyncOperationStatus.Succeeded:
                    AudioClip loadedClip = objs.Result[0];
                    //异步加载远程下载下来的背景音乐
                    //加载完成后播放

                    AudioSource source = soundObj.AddComponent<AudioSource>();
                    source.clip = loadedClip;
                    source.loop = isloop;
                    source.volume = soundValue;
                    source.Play();
                    soundList.Add(source);
                    if (callback != null)
                        callback(source);
                    break;
                default:
                    break;
            }
        });
    }

    /// <summary>
    /// 改变音效大小
    /// </summary>
    /// <param name="value"></param>
    public void ChangeSoundValue(float value)
    {
        soundValue = value;
        for (int i = 0; i < soundList.Count; i++)
            soundList[i].volume = soundValue;
    }

    /// <summary>
    /// 停止音效
    /// </summary>
    public void StopSound(AudioSource source)
    {
        if (soundList.Contains(source))
        {
            soundList.Remove(source);
            source.Stop();
            GameObject.Destroy(source);
        }
    }

    public void ClearSounds()
    {
        soundList.Clear();
    }

}
