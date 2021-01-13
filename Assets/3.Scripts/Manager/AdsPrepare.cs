using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsPrepare : MonoBehaviour, IUnityAdsListener
{
#if UNITY_IOS
    private string gameID = "3883890";
#elif UNITY_ANDROID
    private string gameID = "3883891";
#elif UNITY_EDITOR
    private string gameID = "";
#elif UNITY_STANDALONE_WIN
    private string gameID = "";
#endif

    //HealthBar healthBar;
    string placementID = "rewardedVideo";
    [Header("看完广告给的奖励")]
    public List<GameObject> gifts = new List<GameObject>();


    public void OnUnityAdsDidError(string message)
    {

    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Failed:
                Debug.Log("失败");
                break;
            case ShowResult.Skipped:
                Debug.Log("跳过");
                break;
            case ShowResult.Finished:
                Debug.Log("广告播放完了,给奖励!");
                /*FindObjectOfType<PlayerController>().health = 3;
                FindObjectOfType<PlayerController>().isDead = false;
                float fullHealth = 0;
                if (FindObjectOfType<PlayerController>() != null)
                {
                    fullHealth = FindObjectOfType<PlayerController>().health;
                }
                if (healthBar != null)
                    healthBar.UpdateHealth(fullHealth);*/
                int i = Random.Range(0, gifts.Count - 1);
                 GameObject obj =  GameObject.Instantiate(gifts[i]);
                break;
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {

    }

    public void OnUnityAdsReady(string placementId)
    {
        if (Advertisement.IsReady(placementID))
            Debug.Log("广告准备好了！");
    }

    // Start is called before the first frame update
    void Start()
    {
        //healthBar = FindObjectOfType<HealthBar>();
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameID, true);
    }

    /*private void Update()
    {
        if(healthBar == null)
            healthBar = FindObjectOfType<HealthBar>();
    }*/
}
