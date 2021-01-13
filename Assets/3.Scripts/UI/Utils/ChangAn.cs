using UnityEngine;
public class ChangAn : MonoBehaviour
{
    public float Ping;//长按几秒触发
    private bool IsStart = false;
    private float LastTime = 0;//当前时间
    void Update()
    {
        if (IsStart && Ping > 0 && LastTime > 0 && Time.time - LastTime > Ping)
        {
            Debug.Log("长按触发");
            IsStart = false;
            LastTime = 0;
        }
    }
    public void LongPress(bool bStart)
    {
        IsStart = bStart;
        if (IsStart)
        {
            LastTime = Time.time;
            Debug.Log("长按开始");
        }
        else if (LastTime != 0)
        {
            LastTime = 0;
            Debug.Log("长按取消");
        }
    }
}