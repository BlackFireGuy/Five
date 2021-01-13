using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class PreloadManager : MonoBehaviour
{
    public Text progressTest;//进度

    AsyncOperationHandle downloadDependencies;
    public void Preload()
    {
        StartCoroutine(StartPreload());
    }

    public IEnumerator StartPreload()
    {
        //初始化操作
        AsyncOperationHandle<IResourceLocator> handle = Addressables.InitializeAsync();
        yield return handle;
        Debug.Log("InitializeAsync");
        //清除缓存
        Caching.ClearCache();
        //下载的标签
        string key = "Res";
        AsyncOperationHandle<long> getDownloadSize = Addressables.GetDownloadSizeAsync(key);
        yield return getDownloadSize;

        if(getDownloadSize.Result > 0)
        {
            downloadDependencies = Addressables.DownloadDependenciesAsync(key);
            yield return downloadDependencies;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (downloadDependencies.IsValid())//判断当前下载是否有效
        {
            if (downloadDependencies.PercentComplete < 1)
            {
                
                UpdateProgressBar(downloadDependencies.PercentComplete);
            }else if (downloadDependencies.IsDone)
            {
                Addressables.Release(downloadDependencies);
                Debug.Log("预加载结束");
            }
        }
    }

    private void UpdateProgressBar(float percentComplete)
    {
        progressTest.text = Mathf.CeilToInt(percentComplete * 100f) + "%";
    }

    public void SwitchToHighDef()
    {
        LoadMusic("BK", "Music");
    }

    private void LoadMusic(string key,string label)
    {
        Addressables.LoadAssetsAsync<AudioClip>(new List<object> { key,label},null,Addressables.MergeMode.Intersection).Completed += OnMusicLoaded;
    }
    //唯一的背景音乐组件
    private AudioSource bkMusic = null;
    private void OnMusicLoaded(AsyncOperationHandle<IList<AudioClip>> obj)
    {
        switch (obj.Status)
        {
            case AsyncOperationStatus.Succeeded:
                AudioClip loadedClip = obj.Result[0];
                if (bkMusic == null)
                {
                    GameObject obj11 = new GameObject(PathCfg.OBJECT_MADE_BY_MUSICMGR_FOR_BGM);
                    bkMusic = obj11.AddComponent<AudioSource>();
                }
                //异步加载远程下载下来的背景音乐
                //加载完成后播放
                bkMusic.clip = loadedClip;
                bkMusic.loop = true;
                bkMusic.volume = 0.5f;
                bkMusic.Play();
                break;
            default:
                break;
        }
        
    }
}
