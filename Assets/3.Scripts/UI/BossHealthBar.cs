using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 出现Boss -》UImgr生成bar-》初始化bar
/// </summary>
public class BossHealthBar : BasePanel
{
    Slider holder;
    float maxValue;

    private void Start()
    {
        holder = this.GetControl<Slider>("Holder");
    }

    private void Update()
    {
        if(holder != null)
            holder.maxValue = maxValue;
    }
    /// <summary>
    /// 设置Boss最大生命值
    /// </summary>
    /// <param name="health"></param>
    public void SetBossHealth(float health)
    {
        maxValue = health;
    }
    /// <summary>
    /// 设置Boss当前生命值
    /// </summary>
    /// <param name="health"></param>
    public void UpdateBossHealth(float health)
    {
        holder.value = health;
    }
}
