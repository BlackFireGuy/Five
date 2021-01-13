using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    [Header("运行时分配")]
    public GameObject Dialog;

    TextAsset textfile;

    Sprite face01, face02;
    public UnityAction unityAction;
    public bool isattack;
    public bool isButtonActive;

    public delegate void DialogDelegate();
    public DialogDelegate dialogDelegate;
    //设置对话信息包括响应函数
    public void SetDialogInfo(TextAsset file,Sprite f01, Sprite f02,DialogDelegate _dialogDelegate)
    {
        textfile = file;
        face01 = f01;
        face02 = f02;
        dialogDelegate = _dialogDelegate;
        if(Dialog!=null)
            Dialog.SetActive(true);
    }
    // 显示响应函数
    public void Show()
    {
        dialogDelegate();
    }

    public TextAsset Textfile
    {
        get { return textfile; }
    }

    public Sprite Face01
    {
        get { return face01; }
    }

    public Sprite Face02
    {
        get { return face02; }
    }

}
