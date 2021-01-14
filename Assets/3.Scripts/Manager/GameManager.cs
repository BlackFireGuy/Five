using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("场景设置")]
    public bool isMain;
    public bool isUpDown;
    public bool isInMap;
    public Transform bornPos;
    public static GameManager instance;
    private PlayerController player;

    private Door doorExit;

    public bool gameOver;

    [Header("敌人列表")]
    public List<Enemy> enemies = new List<Enemy>();

    [Header("NPC列表")]
    public List<GameObject> npcs = new List<GameObject>();

    public bool isSkillShoot;
    public bool isEquipEquiped;
    public bool isBossDead;
    bool isgameoverpaenlshowed = false;
    
    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        //当主角死亡并且没有死亡界面时 显示游戏结束UI
        if(player!=null)
            gameOver = player.isDead;
        //UIMgr.instance.GameOverUI(gameOver);
        if (gameOver&&!isgameoverpaenlshowed)
        {
            UIManager.GetInstance().ShowPanel<GameOverPanel>("Game Over Panel", E_UI_Layer.Mid, null);
            isgameoverpaenlshowed = true;
        }
    }
    public void Start()
    {

        //if (isMain) return;
        GameSaveManager.instance.LoadGame();//载入存档，没有存档则载入空存档
        //----------------------------------------------------
        //1先初始化UI
        UIManager.GetInstance().HideAllPanel();
        if(LevelLoader.instance != null)
        {
            LevelLoader.instance.End();
        }//场景切换的过渡

        UIManager.GetInstance().ShowPanel<DialogPanel>("Dialog Panel", E_UI_Layer.Mid, null);//初始化对话窗口
        if (isInMap)
        {
            UIManager.GetInstance().ShowPanel<MapPanel>("Map", E_UI_Layer.Mid, null);
            UIManager.GetInstance().ShowPanel<PauseButton>("Pause Button", E_UI_Layer.Mid, null);
            //UIManager.GetInstance().ShowPanel<BagPanel>("Bag Panel", E_UI_Layer.Mid, null);
            UIManager.GetInstance().ShowPanel<Controller>("Controller", E_UI_Layer.Mid, null);
        }
        else
        {
            UIManager.GetInstance().ShowPanel<ButtonInHome>("Button In Home", E_UI_Layer.Mid, null);
            UIManager.GetInstance().ShowPanel<Controller>("Controller", E_UI_Layer.Mid, null);
            UIManager.GetInstance().ShowPanel<SettingsPanel>("Settings", E_UI_Layer.Mid, null);
            UIManager.GetInstance().ShowPanel<InfoPanel>("Info", E_UI_Layer.Mid, null);
        }
        //----------------------------------------------------
        //再初始化角色和NPC
        if (isUpDown)//第三人称俯视角
        {
            ResMgr.GetInstance().Load<GameObject>("Prefabs/PlayerNew",OnPlayerLoaded);
        }
        else//第三人称2D横板
        {
            Debug.Log("开始实例化！！！");
            ResMgr.GetInstance().Load<GameObject>("BlackMan4", OnPlayerLoaded);

            //GameObject obj = ResMgr.GetInstance().Load<GameObject>("Prefabs/Player/BlackMan4");
            //obj.tranform.position = bornposition
            Debug.Log("已实例化！！！");
        }
            
        //播放音乐
        MusicMgr.GetInstance().PlayBMusic("BK4");
    }
    //角色生成回调函数 当加载完毕再执行
    private void OnPlayerLoaded(AsyncOperationHandle<GameObject> obj)
    {
        switch (obj.Status)
        {
            case AsyncOperationStatus.Succeeded:
                GameObject loadedObject = obj.Result;
                GameObject player = Instantiate(loadedObject, bornPos.position, Quaternion.identity);
                Renderer[] renderers = player.GetComponentsInChildren<SpriteRenderer>();
                foreach (var item in renderers)
                {
                    item.material.shader = Shader.Find("Universal Render Pipeline/2D/Sprite-Lit-Default");
                }
                break;
            default:
                break;
        }
    }


    //---------------------------------------------------------------------------------------------
    //将生成的敌人加入到敌人列表
    public void IsEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }
    //当所有敌人都被打死，则打开传送门
    public void EnemyDead(Enemy enemy)
    {
        enemies.Remove(enemy);
        if(enemies.Count == 0)
        {
            //doorExit.OpenDoor();
            
        }
    }
    //将玩家加入到玩家里
    public void IsPlayer(PlayerController controller)
    {
        player = controller;
    }
    //将门加入到门里
    public void IsDoor(Door door)
    {
        doorExit = door;
    }
    //进入到按序下一个Scene
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    /// <summary>
    /// 从PlayerInfoManager中获取health并返回
    /// </summary>
    /// <returns></returns>
    public float LoadHealth()
    {
        float currentHealth = PlayerInfoManager.instance.info.health;
        return currentHealth;
    }
}
