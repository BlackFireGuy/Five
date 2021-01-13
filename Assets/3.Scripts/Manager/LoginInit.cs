using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginInit : MonoBehaviour
{
    public void Start()
    {
        //1先初始化UI
        UIManager.GetInstance().HideAllPanel();
        //2实例化切换场景工具并且设置跨场景不销毁
        GameObject obj = ResMgr.GetInstance().Load<GameObject>(PathCfg.PATH_UI+ "LevelLoader");
        GameObject.DontDestroyOnLoad(obj);
        UIManager.GetInstance().ShowPanel<Main>("Main", E_UI_Layer.Mid, null);//3Show UI
        MusicMgr.GetInstance().PlayBMusic("BK4");//4播放背景音乐
    }
}
