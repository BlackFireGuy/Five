using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class Loader : MonoBehaviour
{
    public Transform positon;
    
    private void Start()
    {
        //调用OnplayerLoaded
        //Addressables.LoadAssetAsync<GameObject>("BlackMan4").Completed += OnPlayerLoaded;
        Load<GameObject>("BlackMan4",(obj)=> {
            switch (obj.Status)
            {
                case AsyncOperationStatus.Succeeded:
                    GameObject loadedObject = obj.Result;
                    GameObject player = Instantiate(loadedObject, positon.position, Quaternion.identity);
                    Renderer[] renderers = player.GetComponentsInChildren<SpriteRenderer>();
                    foreach (var item in renderers)
                    {
                        item.material.shader = Shader.Find("Universal Render Pipeline/2D/Sprite-Lit-Default");
                    }
                    break;
                default:
                    break;
            }

        });
    }

    //当加载完毕再执行
    private void OnPlayerLoaded(AsyncOperationHandle<GameObject> obj)
    {
        switch (obj.Status)
        {
            case AsyncOperationStatus.Succeeded:
                GameObject loadedObject = obj.Result;
                GameObject player = Instantiate(loadedObject, positon.position, Quaternion.identity);
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

    public T Load<T>(string name) where T : Object
    {
        //Addressabels迁移
        T res = Addressables.LoadAssetAsync<T>(name).Result;
        Debug.Log("已实例化！" + res.name);
        //如果对象是一个GameObject类型 把它实例化后 再返回出去 外部直接使用即可
        if (res is GameObject)
            return GameObject.Instantiate(res);
        else//TestAssset AudioClip
            return res;
    }


    public void Load<T>(string name, System.Action<AsyncOperationHandle<T>> callback) where T : Object
    {
        //T res = Resources.Load<T>(name);
        //Addressabels迁移
        Addressables.LoadAssetAsync<T>(name).Completed += callback;

        //Debug.Log("已实例化！" + res.name);
        //如果对象是一个GameObject类型 把它实例化后 再返回出去 外部直接使用即可
        //if (res is GameObject)
        //    return GameObject.Instantiate(res);
        //else//TestAssset AudioClip
        //    return res;
    }
}
