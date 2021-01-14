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
        ResMgr.GetInstance().Load<GameObject>("LevelLoader",(obj)=> {
            //
            Debug.Log("生成场景切换工具LevelLoader");
            var instance = Instantiate(obj.Result);
            GameObject.DontDestroyOnLoad(instance);
        });
        
        UIManager.GetInstance().ShowPanel<Main>("Main", E_UI_Layer.Mid, null);//3Show UI
        MusicMgr.GetInstance().PlayBMusic("BK4");//4播放背景音乐

        ResMgr.GetInstance().Preload();//5.资源预下载
    }
}
