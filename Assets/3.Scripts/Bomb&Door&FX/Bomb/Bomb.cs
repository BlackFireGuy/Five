using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Animator ani;
    private Collider2D coll;
    private Rigidbody2D rig;
    public float startTime;
    public float waitTime;
    public float bombForce;

    [Header("Check")]
    public float radius;
    public LayerMask targetLayer;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        rig = GetComponent<Rigidbody2D>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ani.GetCurrentAnimatorStateInfo(0).IsName("bomb_off"))
        {
            if (Time.time > startTime + waitTime)
            {
                ani.Play("bomb_explotion");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    public void Explotion()
    {
        MusicMgr.GetInstance().PlaySound("explosion");
        coll.enabled = false;
        Collider2D[] aroundObjects = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);
        
        rig.gravityScale = 0;

        foreach(var item in aroundObjects)
        {
            Vector3 pos = transform.position - item.transform.position;
            item.GetComponent<Rigidbody2D>().AddForce((-pos+Vector3.up) * bombForce, ForceMode2D.Impulse);
            if (item.CompareTag("Bomb") && item.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("bomb_off"))
            {
                item.GetComponent<Bomb>().TurnOn();
            }
            if (item.CompareTag("Player"))
            {
                item.GetComponent<IDamageable>().GetHit(3);
            }
        }
        
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
        //从NPCList中去掉
    }
    public void TurnOff()
    {
        ani.Play("bomb_off");
        gameObject.layer = LayerMask.NameToLayer("NPC");
    }
    public void TurnOn()
    {
        startTime = Time.time;
        ani.Play("bomb_on");
        gameObject.layer = LayerMask.NameToLayer("Bomb");
    }
}
