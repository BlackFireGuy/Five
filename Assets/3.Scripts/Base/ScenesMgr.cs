using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景切换模块
/// 知识点
/// 1.场景异步加载
/// 2.协程
/// 3.委托
/// </summary>
public class ScenesMgr : BaseSingleton<ScenesMgr>
{
    public float progress;

    /// <summary>
    /// 切换场景 同步
    /// </summary>
    /// <param name="name"></param>
    public void LoadScene(string name,UnityAction fun)
    {
        //场景同步加载
        SceneManager.LoadScene(name);
        //加载完成过后 才回去执行fun
        if(fun != null)
            fun();
    }
    public void LoadScene(int id, UnityAction fun)
    {
        //场景同步加载
        SceneManager.LoadScene(id);
        //加载完成过后 才回去执行fun
        if(fun!=null)
            fun();
    }
/*
    /// <summary>
    /// 提供给外部的异步加载的接口方法
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fun"></param>
    public void LoadSceneAsyn(string name,UnityAction fun)
    {
        MonoMgr.GetInstance().StartCoroutine(ReallyLoadSceneAsyn(name, fun));
    }

    /// <summary>
    /// 协程异步加载场景
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fun"></param>
    /// <returns></returns>
    private IEnumerator ReallyLoadSceneAsyn(string name, UnityAction fun)
    {
        GameObject obj = ResMgr.GetInstance().LoadAsync<GameObject>("Prefabs/UI/Load Scene");
        AsyncOperation ao =  SceneManager.LoadSceneAsync(name);
        //UIManager.GetInstance().ShowPanel<LoadSceneSliderPanel>("Load Scene Slider", E_UI_Layer.Top, Callback);
        
        //可以得到场景加载的进度
        while (!ao.isDone)
        {
            //事件中心向外分发进度情况 外面想用就用
            EventCenter.GetInstance().EventTrigger(EventCfg.SCENE_LOADASY_PROCESS,ao.progress);
            //这里面去更新进度条
            progress = ao.progress/0.9f;
            
            yield return ao.progress;
        }
        yield return ao;
        //加载完成过后 才回去执行fun
        if (fun != null)
            fun();
    }
*/
    private void Callback(LoadSceneSliderPanel panel)
    {
        //换刀basepanel里面了
        /*panel.slider.value = progress;
        panel.progressText.text = Mathf.FloorToInt(progress * 100f).ToString() + "%";*/
    }

    /// <summary>
    /// 重新加载当前场景
    /// </summary>
    public void RestartSccene()
    {
        //LoadSceneAsyn(SceneManager.GetActiveScene().name, null) ;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //PlayerPrefs.DeleteKey("playerHealth");
    }
}
