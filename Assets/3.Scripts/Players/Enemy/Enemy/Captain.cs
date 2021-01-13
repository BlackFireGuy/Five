using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Captain : Enemy, IDamageable
{
    SpriteRenderer render;
    


    public override void Init()
    {
        base.Init();
        render = GetComponent<SpriteRenderer>();

    }
    public override void Update()
    {
        base.Update();
        if (aniState == 0)
            render.flipX = false;
    }

    public void GetHit(float damage)
    {
        health -= damage;
        GetComponentInChildren<HealthBarSlider>().hp = health;
        if (health < 1)
        {
            health = 0;
            isDead = true;
        }
        ani.SetTrigger("hit");
    }

    public override void SkillAction()
    {
        base.SkillAction();
        if (ani.GetCurrentAnimatorStateInfo(1).IsName("Captain_skill"))//播放完动画的时间
        {
            if(transform.position.x > targetPoint.position.x)
            {
                render.flipX = true;
                transform.position = Vector2.MoveTowards(transform.position,transform.position + Vector3.right, speed * 2 * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.left, speed * 2 * Time.deltaTime);
            }
        }
        else
        {
            render.flipX = false;
        }
    }
}
