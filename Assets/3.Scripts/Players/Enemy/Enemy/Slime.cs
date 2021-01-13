using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy, IDamageable
{
    public void GetHit(float damage)
    {
        MusicMgr.GetInstance().PlaySound("Po");
        damage -= armor;
        if (damage > 0)
        {
            health -= damage;
            healthBar.hp = health;
            if (health < 1)
            {
                health = 0;
                isDead = true;
            }
            ani.SetTrigger("hit");
        }
    }
    public override void Init()
    {
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        
        
        if (healthBar != null)
        {
            //不同生物，生命值随着主角的提升而提升
            //healthBar.maxHp = health;
            health = health * (1 + (PlayerInfoManager.instance.info.pastSucess + PlayerInfoManager.instance.info.frontSucess)/10);
            healthBar.maxHp = health;
            //攻击力也提升
            hit.damage = hit.damage * (1 + (PlayerInfoManager.instance.info.pastSucess + PlayerInfoManager.instance.info.frontSucess) / 10);
            //防御也提升
            armor = armor * (1 + (PlayerInfoManager.instance.info.pastSucess + PlayerInfoManager.instance.info.frontSucess) / 10);
            //移动速度也提升
            //史莱姆移动速度不变
        }
         //alarmSigh = transform.GetChild(0).gameObject;
        
        if (isUpDown)
            rig.gravityScale = 0;
    }

    public void PlayAttackSound()
    {
        MusicMgr.GetInstance().PlaySound("Bo");
    }
}
