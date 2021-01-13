using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 攻击 各种攻击姿势
/// </summary>
public class ArmourHit : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator ani;
    AnimatorOverrideController overrideController;
    RuntimeAnimatorController tempController;

    public bool isPlayer;
    [Header("动画片段-轻击")]
    public AnimationClip[] attacks_lightly;//默认up,middle,down,upM,downM
    [Header("动画片段-普通攻击")]
    public AnimationClip[] attacks_normally;
    [Header("动画片段-重击")]
    public AnimationClip[] attacks_heavily;
    string attackClipNameLightly = "attack_middle_forward_lightly";
    string attackClipNameNormally = "attack_middle_forward_normal";
    string attackClipNameHeavilly = "attack_middle_forward_heavily";
    [Header("技能：前冲距离/面向方向")]
    public float speed=1f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        tempController = ani.runtimeAnimatorController;
        overrideController = new AnimatorOverrideController();
        overrideController.runtimeAnimatorController = tempController;


        AddAnimationEvents();

        
    }

    private void Update()
    {
        ani.SetBool("isAttacking", BeatManager.instance.isAttacking);
    }

    public void Attack()
    {
        //根据连击层数和击打方向改变击打动作
        switch (BeatManager.instance.dir)
        {
            case BeatDir.up:
                PlayAnimation(attackClipNameLightly, attacks_lightly[0]);
                PlayAnimation(attackClipNameNormally, attacks_normally[0]);
                PlayAnimation(attackClipNameHeavilly, attacks_heavily[0]);
                break;
            case BeatDir.middle:
                PlayAnimation(attackClipNameLightly, attacks_lightly[1]);
                PlayAnimation(attackClipNameNormally, attacks_normally[1]);
                PlayAnimation(attackClipNameHeavilly, attacks_heavily[1]);
                break;
            case BeatDir.down:
                PlayAnimation(attackClipNameLightly, attacks_lightly[2]);
                PlayAnimation(attackClipNameNormally, attacks_normally[2]);
                PlayAnimation(attackClipNameHeavilly, attacks_heavily[2]);
                break;
            case BeatDir.upM:
                break;
            case BeatDir.downM:
                break;
        }
        
    }
    //-----------------------------------------------
    //events
    void SetIsMoveTrue()
    {
        rb.velocity = new Vector2(BeatManager.instance.playerForwardDir * speed, rb.velocity.y);
    }
    void SetIsMoveFalse()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
    }
    void CurrentAttack(int i)
    {
        BeatManager.instance.currentAttack=i;
    }
    //给动画添加events
    private void AddAnimationEvents()
    {
        AnimationEvent newEvent = new AnimationEvent();
        newEvent.functionName = "SetIsMoveTrue";
        newEvent.time = 0.0f;
        AnimationEvent newEvent1 = new AnimationEvent();
        newEvent1.functionName = "SetIsMoveFalse";
        newEvent1.time = 0.12f;
        AnimationEvent newEvent2 = new AnimationEvent();
        newEvent2.functionName = "CurrentAttack";
        newEvent2.time = 0.0f;
        
        for (int i = 0; i < attacks_lightly.Length; i++)
        {
            attacks_lightly[i].AddEvent(newEvent);
            attacks_lightly[i].AddEvent(newEvent1);
            newEvent2.intParameter = 1;
            attacks_lightly[i].AddEvent(newEvent2);
        }
        for (int i = 0; i < attacks_normally.Length; i++)
        {
            attacks_normally[i].AddEvent(newEvent);
            attacks_normally[i].AddEvent(newEvent1);
            newEvent2.intParameter = 2;
            attacks_normally[i].AddEvent(newEvent2);
        }
        for (int i = 0; i < attacks_heavily.Length; i++)
        {
            attacks_heavily[i].AddEvent(newEvent);
            attacks_heavily[i].AddEvent(newEvent1);
            newEvent2.intParameter = 3;
            attacks_heavily[i].AddEvent(newEvent2);
        }
    }
    //播放动画
    private void PlayAnimation(string clipName,AnimationClip animationClip)
    {
        overrideController[clipName] = animationClip;
        ani.runtimeAnimatorController = overrideController;
        ani.SetTrigger("attack");
        
    }
}
