using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GlobalVolumeManager : MonoBehaviour
{

    public static GlobalVolumeManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
                Destroy(this);
        }
    }
    Volume volume;
    List<VolumeComponent> list;

    float goaltime;
    [Header("后处理参数")]
    float shakeAmp = 0;
    bool isShake;
    private void Start()
    {
        volume = GetComponent<Volume>();
        list = volume.profile.components;
        
    }


    private void Update()
    {
        if (isShake)
        {
            shakeAmp = Mathf.Min(goaltime - Time.time, shakeAmp);
            if (list[2].parameters[2].overrideState)
                list[2].parameters[2].SetValue(new FloatParameter(shakeAmp));
            if (shakeAmp == 0) isShake = false;
        }
        
    }
    public void Shake()
    {
        goaltime = Time.time + 0.5f;
        shakeAmp = 0.5f;
        isShake = true;
    }
}
