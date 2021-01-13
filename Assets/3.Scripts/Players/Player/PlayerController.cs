using UnityEngine;
/// <summary>
/// 控制行走行动，跳跃，和攻击的中枢转移控制
/// </summary>
[RequireComponent(typeof(ArmourHit))]
[RequireComponent(typeof(BodyHit))]
public class PlayerController : MonoBehaviour, IDamageable
{
    private Rigidbody2D rb;
    private Animator ani;
    private ArmourHit ahit;
    private BodyHit bhit;
    private  FloatingJoystick joystick;
    private HealthBarSlider healthBar;


    public float swimSpeed = 1;
    public float speed;
    public float jumpForce;
    [Header("是否为俯视角")]
    public bool isUpDown;//是否是俯视视角移动

    [Header("玩家状态")]
    public float health;
    public bool isDead;

    [Header("状态标识")]
    public bool canJump;

    [Header("动作特效")]
    public GameObject jumpFX;
    public GameObject landFX;

    [Header("攻击设置")]
    public GameObject bombPrefab;
    public float nextAttack = 0;
    public float attackRate;
    public Transform ShootParent;

    [Header("技能和装备")]
    public Inventory inventorySkill;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        ahit = GetComponent<ArmourHit>();
        bhit = GetComponent<BodyHit>();
        joystick = FindObjectOfType<FloatingJoystick>();
        healthBar = GetComponentInChildren<HealthBarSlider>();
        
        GameManager.instance.IsPlayer(this);//告诉游戏管理这我就是玩家
        //初始化玩家血量
        health = GameManager.instance.LoadHealth();
        healthBar.maxHp = health;
        //检测是否装备了技能,.是则装备
        for (int i = 0; i < inventorySkill.itemList.Count; i++)
        {
            if (inventorySkill.itemList[i].isEquip)//这个是装备的
            {
                PlayerInfoManager.instance.infoItemData = inventorySkill.itemList[i];
                //PoolMgr.GetInstance().GetObj(SkillTable.skillpath[inventorySkill.itemList[i].skillId], SkillInit);
               
                GameObject obj = ResMgr.GetInstance().Load<GameObject>(SkillTable.skillpath[inventorySkill.itemList[i].skillId]);
                SkillInit(obj);
                GameManager.instance.isSkillShoot = true;
            }
        }

    }
    
    //死亡不进行控制
    void Update()
    {
        ani.SetBool("dead", isDead);

        if (isDead) return;
        if (bhit.beAttacking) return;
        if (joystick == null)
            joystick = FindObjectOfType<FloatingJoystick>();

    }
    // 死亡或者失控则不进行控制
    //固定时长执行，跟物理有关的
    private void FixedUpdate()
    {
        //是否死亡
        if (isDead)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        //是否失控
        if (bhit.beAttacking) return;

        MoveController();
    }


    //--------------------------------------------------------------------------
    //玩家移动动作
    void MoveController()
    {

        ani.SetFloat("test", Input.GetAxis("Horizontal"));
        //操作杆
        if (joystick != null)
        {
            float horizontalInput = joystick.Horizontal;
            if (horizontalInput > 0)
            {
                horizontalInput = 1;
                BeatManager.instance.playerForwardDir = 1;
                transform.eulerAngles = new Vector3(0, 0, 0);
                healthBar.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            if (horizontalInput < 0)
            {
                horizontalInput = -1;
                BeatManager.instance.playerForwardDir = -1;
                transform.eulerAngles = new Vector3(0, 180, 0);
                //血条不动
                healthBar.transform.localEulerAngles = new Vector3(0, 180, 0);
            }
            if (horizontalInput < 0.0001f&&horizontalInput>-0.0001f) return;
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        }
    }
    //玩家跳跃动作
    public void JumpController()
    {
        if (joystick != null)
        {
            if (bhit.isGround) canJump = true;
            if (canJump)
            {
                ani.SetTrigger("jump");
            }
        }
    }
    // 玩家攻击动作(除受伤外）
    public void Attack()
    {
        if (isDead) return;
        if (Time.time > nextAttack)
        {
            //这里可以用缓存池和
            ahit.Attack();
            nextAttack = Time.time + attackRate;
            //后续设计到技能上
            /*Instantiate(bombPrefab, transform.position, bombPrefab.transform.rotation);
            nextAttack = Time.time + attackRate;*/
        }
    }


    //--------------------------------------------------------------------------
    // 搭载装备的实现
    private void SkillInit(GameObject arg0)
    {
        arg0.transform.SetParent(ShootParent);
        arg0.transform.localPosition = Vector3.zero;
        arg0.transform.localScale = Vector3.one;
    }
    //受伤的playercontroller内实现
    public void GetHit(float damage)
    {
        if (!ani.GetCurrentAnimatorStateInfo(1).IsName("player_hit"))
        {
            MusicMgr.GetInstance().PlaySound("hurt");

            health -= damage;
            if (health < 1)
            {
                health = 0;
                isDead = true;
            }
            ani.SetTrigger("hit");

            healthBar.hp = health;
        }
    }


    //--------------------------------------------------------------------------
    // animation event
    public void LandFX()
    {
        landFX.SetActive(true);
        landFX.transform.position = transform.position + new Vector3(0, -0.75f, 0);
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        canJump = false;
        jumpFX.SetActive(true);
        jumpFX.transform.position = transform.position + new Vector3(0, -0.45f, 0);
        
    }
}
