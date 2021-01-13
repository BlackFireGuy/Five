using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : BasePanel
{
    Button tryAgain,mainMenu;

    void Start()
    {
        tryAgain = this.GetControl<Button>("Try Again");
        //watchAds = this.GetControl<Button>("Watch Ads");
        mainMenu = this.GetControl<Button>("Main Menu");

        tryAgain.onClick.AddListener(TryAgain);
        //watchAds.onClick.AddListener(WatchAds);
        mainMenu.onClick.AddListener(MainMenu);
    }

  /*  private void WatchAds()
    {
        AdsManager.GetInstance().ShowRewardAds();
        UIManager.GetInstance().HidePanel("Game Over Panel");
    }*/

    private void TryAgain()
    {
        UIManager.GetInstance().HideAllPanel();
        
        UIManager.GetInstance().HidePanel("Game Over Panel");
        MusicMgr.GetInstance().ClearSounds();
        //似乎一定要放到最后
        //ScenesMgr.GetInstance().RestartSccene();
        LevelLoader.instance.LoadNextLevel(SceneManager.GetActiveScene().buildIndex);
    }

    private void MainMenu()
    {
        UIManager.GetInstance().HideAllPanel();
        
        UIManager.GetInstance().HidePanel("Game Over Panel");
        MusicMgr.GetInstance().ClearSounds();
        //似乎一定要放到最后
        //ScenesMgr.GetInstance().LoadScene("Home", null);
        LevelLoader.instance.LoadNextLevel("Home");
    }
}
