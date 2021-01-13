using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class DialogButton : MonoBehaviour
{
    public GameObject Button;
    //public GameObject talkUI;
    [Header("文本文件")]
    public TextAsset textfile;

    [Header("头像")]
    public Sprite face01, face02;

    public bool beginTalk;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Button.SetActive(true);
            DialogManager.instance.isButtonActive = true;
        }
            

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Button.SetActive(false);
            DialogManager.instance.isButtonActive = false;
        }
            


    }

    private void Update()
    {
        if (Button.activeSelf && (Input.GetKeyDown(KeyCode.R)|| DialogManager.instance.isattack))
        {
            //talkUI.SetActive(true);
            DialogManager.instance.SetDialogInfo(textfile, face01, face02, Show);
        }
    }

    public virtual void Show()
    {
        
    }
    
}
