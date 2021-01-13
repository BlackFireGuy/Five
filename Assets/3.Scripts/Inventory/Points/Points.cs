using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    string path = "Prefabs/EnvironmentTools/shanshan";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (PlayerInfoManager.instance.info.points < 20)
            {
                PlayerInfoManager.instance.info.points++;
                PoolMgr.GetInstance().PushObj(path,this.gameObject);
            }
        }
    }
}
