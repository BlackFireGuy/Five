using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

/// <summary>
/// 资源加载模块
/// 1.异步加载
/// 2.委托和lambda表达式
/// 3.协程
/// 4.泛型
/// 
/// Addressables CCD 预下载
/// </summary>
public class ResMgr : BaseSingleton<ResMgr>
{
    AsyncOperationHandle downloadDependencies;

    //加载资源
    public void Load<T>(string name,System.Action<AsyncOperationHandle<T>> callback) where T : Object
    {
        //T res = Resources.Load<T>(name);
        //Addressabels迁移
        Addressables.LoadAssetAsync<T>(name).Completed += callback;

        //Debug.Log("已实例化！" + res.name);
        //如果对象是一个GameObject类型 把它实例化后 再返回出去 外部直接使用即可
        //if (res is GameObject)
        //    return GameObject.Instantiate(res);
        //else//TestAssset AudioClip
        //    return res;
    }
    //--------------------------------------------------------------------------------------------------------------
     
    //异步加载资源 其实是一样的
    public void LoadAsync<T>(string name, System.Action<AsyncOperationHandle<T>> callback) where T : Object
    {
        //开启异步加载的协程
        MonoMgr.GetInstance().StartCoroutine(ReallyLoadAsync(name, callback));
    }

    //真正的协同程序函数，用于开启异步加载对应的资源
    private IEnumerator ReallyLoadAsync<T>(string name, System.Action<AsyncOperationHandle<T>> callback) where T : Object
    {
        //ResourceRequest r = Resources.LoadAsync<T>(name);
        //迁移
        Addressables.LoadAssetAsync<T>(name).Completed += callback;
        yield return null;
/*
        yield return r;

        if (r.asset is GameObject)
            callback(GameObject.Instantiate(r.asset) as T);
        else
            callback(r.asset as T);*/
    }

    //-------------------------------
    //异步加载复数资源
    public void LoadAssetsAsync<T>(string label, System.Action<AsyncOperationHandle<IList<T>>> callback) where T : Object
    {
        //开启异步加载的协程
        Addressables.LoadAssetsAsync<T>(new List<object> { label }, null, Addressables.MergeMode.Intersection).Completed += callback;
    }
    public void Preload()
    {
        MonoMgr.GetInstance().StartCoroutine(StartPreload());
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

        if (getDownloadSize.Result > 0)
        {
            downloadDependencies = Addressables.DownloadDependenciesAsync(key);
            yield return downloadDependencies;
        }
    }

    public float ReportProgress()
    {
        if (downloadDependencies.IsValid())//判断当前下载是否有效
        {
            if (downloadDependencies.PercentComplete < 1)
            {
                return(downloadDependencies.PercentComplete);
            }
            else if (downloadDependencies.IsDone)
            {
                Debug.Log("预加载结束");
                return(1);
            }
        }
        return (1);
    }

}
