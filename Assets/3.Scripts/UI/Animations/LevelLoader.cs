using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : BasePanel
{
    public static LevelLoader instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }


    public float transitionTime = 1f;
    public Animator transition;

    public void LoadNextLevel(string name)
    {
        StartCoroutine(LoadLevel(name));
    }
    public void LoadNextLevel(int id)
    {
        //清空缓存池
        PoolMgr.GetInstance().Clear();
        //清空音效池
        MusicMgr.GetInstance().ClearSounds();
        
        StartCoroutine(LoadLevel(id));
    }
    IEnumerator LoadLevel(string name)
    {
        transition.SetTrigger("start");

        yield return new WaitForSeconds(transitionTime);

        ScenesMgr.GetInstance().LoadScene(name,null); ;
    }
    IEnumerator LoadLevel(int id)
    {
        transition.SetTrigger("start");

        yield return new WaitForSeconds(transitionTime);

        ScenesMgr.GetInstance().LoadScene(id, null); ;
    }
    public void End()
    {
        transition.SetTrigger("end");
    }
}
