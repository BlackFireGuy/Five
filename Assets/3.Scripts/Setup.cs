using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup : MonoBehaviour
{
    GameObject _gameManager;
    private void Awake()
    {
        //闪屏、更新、最后登录
        
        //初始化一个GameManager
        if (_gameManager == null)
        {
            _gameManager = new GameObject("GameManager");
            GameObject.DontDestroyOnLoad(_gameManager);
            _gameManager.AddComponent<GameManager>();
        }
    }
}
