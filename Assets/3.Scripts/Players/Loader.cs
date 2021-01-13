using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class Loader : MonoBehaviour
{
    public AssetReference playerPrefab;
    public Transform positon;
    
    private void Start()
    {
        //调用OnplayerLoaded
        Addressables.LoadAssetAsync<GameObject>(playerPrefab).Completed += OnplayerLoaded;
    }

    //当加载完毕再执行
    private void OnplayerLoaded(AsyncOperationHandle<GameObject> obj)
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
}
