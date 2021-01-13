using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : Enemy, IDamageable
{
    public float scale;

    public override void Init()
    {
        base.Init();
        if (isBoss)
        {
            //死了掉奖励，并打开返回的大门
        }
    }


    public void GetHit(float damage)
    {
        health -= damage;
        if (health < 1)
        {
            health = 0;
            isDead = true;
        }
        ani.SetTrigger("hit");
    }

    public void Swalow()
    {
        targetPoint.GetComponent<Bomb>().TurnOff();
        targetPoint.gameObject.SetActive(false);
        transform.localScale *= scale;
    }
}
