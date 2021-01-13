using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : BasePanel
{
    Button  playAgain, mainMenu;

    private void Start()
    {
        //resume = this.GetControl<Button>("Resume");
        playAgain = this.GetControl<Button>("Play Again");
        mainMenu = this.GetControl<Button>("Main Menu");

        //resume.onClick.AddListener(ResumeGame);
        playAgain.onClick.AddListener(PlayAgain);
        mainMenu.onClick.AddListener(GoToMainMenu);
    }

    private void PlayAgain()
    {
        Time.timeScale = 1;
        //ScenesMgr.GetInstance().RestartSccene();
        LevelLoader.instance.LoadNextLevel(SceneManager.GetActiveScene().buildIndex);
        MusicMgr.GetInstance().ClearSounds();
    }

    private void GoToMainMenu()
    {
        //恢复时间流动
        Time.timeScale = 1;
        //清空音效
        MusicMgr.GetInstance().ClearSounds();
        //载入场景
        //ScenesMgr.GetInstance().LoadScene(0, null);
        //ScenesMgr.GetInstance().LoadScene("Home", null);
        LevelLoader.instance.LoadNextLevel("Home");
        //保存背包
        //GameSaveManager.GetInstance().SaveGame();

    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        UIManager.GetInstance().HidePanel("Pause Menu");
    }
}
