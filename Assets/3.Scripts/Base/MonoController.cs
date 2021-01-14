﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Mono的管理者
/// 1.生命周期函数
/// 2.事件
/// 3.协程
/// </summary>
public class MonoController : MonoBehaviour
{
    public event UnityAction updateEvent;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (updateEvent != null)
            updateEvent();

        
    }

    /// <summary>
    /// 给外部提供的 添加帧更新事件的函数
    /// </summary>
    /// <param name="fun"></param>
    public void AddUpdateListener(UnityAction fun)
    {
        updateEvent += fun;
    }

    /// <summary>
    /// 提供给外部，用于移除帧更新事件函数
    /// </summary>
    /// <param name="fun"></param>
    public void RemoveUpdateListener(UnityAction fun)
    {
        updateEvent -= fun;
    }
    /// <summary>
    /// 提供给外部，用于生成GameObject
    /// </summary>
    /// <param name="obj"></param>
    public GameObject InstantiateObj(GameObject obj)
    {
        return Instantiate(obj);
    }


}



