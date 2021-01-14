using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreloadUIManager : MonoBehaviour
{
    public Text progressTest;//进度
    // Update is called once per frame
    void Update()
    {
        UpdateProgressBar(ResMgr.GetInstance().ReportProgress());
    }
    private void UpdateProgressBar(float percentComplete)
    {
        progressTest.text = Mathf.CeilToInt(percentComplete * 100f) + "%";
    }
}
