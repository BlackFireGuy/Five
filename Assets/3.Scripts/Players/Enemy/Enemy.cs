using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    EnemyBaseState currentState;
    [Header("运行时分配")]
    public Animator ani;
    public int aniState;
    public Rigidbody2D rig;

    public bool isDead;
    public bool hasBomb;
    
    public BossHealthBar bossHealthBar;
    
    public List<Transform> attackList = new List<Transform>();
    [Header("运行前设置")]
    public HealthBarSlider healthBar;
    public GameObject alarmSigh;
    public HitPoint hit;
    [Header("Base State")]
    public float health;
    public float armor;
    public bool isBoss;
    [Header("Movement")]
    public float speed;
    public Transform pointA, pointB;
    public Transform targetPoint;
    public bool isUpDown;
    //public float distance;

    [Header("Attack Setting")]
    public float attackRate;
    private float nextAttack = 0;
    public float attackRange, skillRange;
    /*public GameObject hitDialog;
    public Text hitNum;
    public float lifeTimer;
    public float upSpeed;*/

    [Header("奖励列表")]
    public List<GameObject> gifts = new List<GameObject>();
    public int giftMaxNum;


    
    public PatrolState patrolState = new PatrolState();
    public AttackState attackState = new AttackState();

    bool isGifted  = false;
    
    string path = "Prefabs/EnvironmentTools/shanshan";//天赋路径
    public virtual void Init()
    {
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();

        healthBar = GetComponentInChildren<HealthBarSlider>();
        if(healthBar != null)
        {
            healthBar.maxHp = health;
        }

        alarmSigh = transform.GetChild(0).gameObject;

        if (isUpDown)
            rig.gravityScale = 0;
    }

    public void SetHealth(BossHealthBar bar)
    {
        bar.SetBossHealth(health);
    }

    private void Awake()
    {
        Init();
    }
    private void Start()
    {
        GameManager.instance.IsEnemy(this);
        TransitionToState(patrolState);

    }

    public virtual void Update()
    {
        if (isBoss)
        {
            if (isDead)
                GameManager.instance.isBossDead = true;
        }
        LinkYwithOrderinlayer();
        ani.SetBool("dead", isDead);
        if (isDead)
        {
            if(healthBar != null)
                healthBar.gameObject.SetActive(false);
            if(isBoss)
                bossHealthBar.UpdateBossHealth(health);
            GameManager.instance.EnemyDead(this);
            //掉落物体
            if(!isGifted)
                GiveGifts();
            return;
        }
        currentState.OnUpdate(this);
        ani.SetInteger("state", aniState);
        if (isBoss)
        {
            if (bossHealthBar == null)
                bossHealthBar = FindObjectOfType<BossHealthBar>();
            else
                bossHealthBar.UpdateBossHealth(health);
        }
            
    }

    private void GiveGifts()
    {
        PoolMgr.GetInstance().GetObj(path,Init);
        isGifted = true;
        if (gifts == null) return;
        if (giftMaxNum < 1) return;
        int n = Random.Range(1, giftMaxNum);

        for (int i = 0; i < n; i++)
        {
            int j = Random.Range(0, gifts.Count - 1);
            GameObject obj = Instantiate(gifts[j]);
            obj.transform.position = transform.position;

        }
        

        
    }

    private void Init(GameObject arg0)
    {
        arg0.transform.position = this.transform.position;
    }

    public void TransitionToState(EnemyBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
    public void MoveToTarget()
    {
        /*Vector3 dir = (targetPoint.position - transform.position).normalized*distance;
        transform.position = Vector2.MoveTowards(transform.position,targetPoint.position-dir, speed * Time.deltaTime);//* Time.deltaTime在不同机器上获得同样的效果
        FilpDirection();*/
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position , speed * Time.deltaTime);//* Time.deltaTime在不同机器上获得同样的效果
        FilpDirection();

    }

    public  virtual void AttackAction()//攻击玩家
    {
        if (Vector2.Distance(transform.position, targetPoint.position) < attackRange)
        {
            if (Time.time > nextAttack)
            {
                //播放攻击动画
                ani.SetTrigger("attack");
                nextAttack = Time.time + attackRate;
            }
        }
    }
    public virtual void SkillAction()//对炸弹使用技能，每个敌人不同的方式
    {
        if (Vector2.Distance(transform.position, targetPoint.position) < skillRange)
        {
            if (Time.time > nextAttack)
            {
                //播放攻击动画
                ani.SetTrigger("skill");
                nextAttack = Time.time + attackRate;
            }
        }
    }
    public void FilpDirection()
    {
        if (transform.position.x < targetPoint.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if (healthBar != null)
                healthBar.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            if (healthBar != null)
                healthBar.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
            
    }
    public void SwitchPoint()
    {
        if(Mathf.Abs(pointA.position.x-transform.position.x)> Mathf.Abs(pointB.position.x - transform.position.x))
        {
            targetPoint = pointA;
        }
        else
        {
            targetPoint = pointB;
        }
    }

    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!attackList.Contains(collision.transform)&&!hasBomb&&!isDead&&!GameManager.instance.gameOver&&collision.CompareTag("Player"))
            attackList.Add(collision.transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        attackList.Remove(collision.transform);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead&& !GameManager.instance.gameOver)
            StartCoroutine(OnAlarm());
        if (collision.CompareTag("Player"))
        {
            if (isBoss)
            {
                UIManager.GetInstance().ShowPanel<BossHealthBar>("Boss Health Bar", E_UI_Layer.Top, SetHealth);
            }
        }
        
    }

    IEnumerator OnAlarm()
    {
        alarmSigh.SetActive(true);
        yield return new WaitForSeconds(alarmSigh.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length);//返回的是baselayer的唯一的一个动画的片段的长度
        alarmSigh.SetActive(false);
    }
    protected void LinkYwithOrderinlayer()
    {
        //chartransform为角色的transform组件
        float yPos = transform.position.y;
        //spriterenderer为角色的Sprite Renderer组件
        //通过改变sortingorder来确定gameobject在sortinglayer中的order in layer
        //因为y轴越大层级越靠后，所以给结果前加一个负数
        //scale用来扩大y轴数据映射在层级上的影响，根据游戏中实际使用的素材尺度来确定，取100 1000都可以
        GetComponent<SpriteRenderer>().sortingOrder = -Mathf.RoundToInt(yPos * 1000);
    }
    IEnumerator WaitForAnimationPlayOver(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
