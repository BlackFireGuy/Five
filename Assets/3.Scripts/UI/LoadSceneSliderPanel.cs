using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSceneSliderPanel : BasePanel
{
    public Slider slider;

    public Text progressText;

    private void Update()
    {
        slider.value = ScenesMgr.GetInstance().progress;
        progressText.text = Mathf.FloorToInt((ScenesMgr.GetInstance().progress) * 100f).ToString() + "%";
    }


}
