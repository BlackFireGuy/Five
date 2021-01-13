using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : BasePanel
{
    public GameObject normalAttack;

    public GameObject shootAttack;

    Button skill, attack ,jump;

    PlayerController player;

    public float Ping;//长按几秒触发
    private bool IsStart = false;
    private float LastTime = 0;//当前时间
    void Start()
    {
        //skill = this.GetControl<Button>("Skill");
        attack = this.GetControl<Button>("Attack");
        jump = this.GetControl<Button>("Jump");

        //skill.onClick.AddListener(SkillAttack);
        

        attack.onClick.AddListener(NormalAttack);
        jump.onClick.AddListener(Jump);
    }

    private void Jump()
    {
        //角色放技能
        if (player != null)
        {
            player.JumpController();
        }
    }

    private void Update()
    {
        if(player == null)
            player = FindObjectOfType<PlayerController>();


        if (GameManager.instance.isSkillShoot)
        {
            normalAttack.SetActive(false);
            shootAttack.SetActive(true);
        }
        else
        {
            normalAttack.SetActive(true);
            shootAttack.SetActive(false);
        }

        if (IsStart && Ping > 0 && LastTime > 0 && Time.time - LastTime > Ping)
        {
            Debug.Log("长按触发");
            
            IsStart = false;
            LastTime = 0;
        }

    }

    private void NormalAttack()
    {
        if (player != null)
        {
            //player.ButtonJump();
            player.Attack();
        }
    }

    private void SkillAttack()
    {
        
        
    }
    //-----------------------------------------------------------------------------------------
    //event
    public void PutDown()
    {
        //isdown = true;
        //time = Time.time;
        DialogManager.instance.isattack = true;
    }
    public void PutUp()
    {
        //isdown = false;
        //time = 0;
        DialogManager.instance.isattack = false;
    }
    public void LongPress(bool bStart)
    {
        IsStart = bStart;
        if (IsStart)
        {
            LastTime = Time.time;
            Debug.Log("长按开始");


        }
        else if (LastTime != 0)
        {
            LastTime = 0;
            Debug.Log("长按取消");
        }
    }


}
