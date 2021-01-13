using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : BasePanel
{
    Button newgame,continu,quit;
    
    void Start()
    {
        newgame = this.GetControl<Button>("New Game");
        continu = this.GetControl<Button>("Continue");
        quit = this.GetControl<Button>("Quit");

        newgame.onClick.AddListener(NewGame);
        continu.onClick.AddListener(ContinueGame);
        quit.onClick.AddListener(QuitGame);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        //1进入Home场景
        //2如果有存档则载入、载入个人信息
        if (GameSaveManager.instance.IsSaved())//有存档
        {
            //Debug.Log("载入存档");
            NewGameSet();
            /*GameSaveManager.instance.LoadGame();*/
        }
        else//无存档
        {
            //Debug.Log("载入新存档");
            GameSaveManager.instance.NewGame();
            NewGameSet();
        }
        

    }
    //开始新的游戏
    public void NewGame()
    {
        GameSaveManager.instance.NewGame();
        NewGameSet();
    }
    //新游戏设置
    public void NewGameSet()
    {
        //Debug.Log("载入新存档");
        PlayerPrefs.DeleteAll();
        //ScenesMgr.GetInstance().LoadScene("Home", null);
        LevelLoader.instance.LoadNextLevel("Home");
        UIManager.GetInstance().HidePanel("Main");
    }
}
