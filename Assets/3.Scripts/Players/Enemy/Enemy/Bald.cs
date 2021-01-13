using UnityEngine;

public class Bald : Enemy, IDamageable
{
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
    //非碰撞体效果在这里写
}
