using System.Collections;
using UnityEngine;
/// <summary>
/// 受击 根据位置和情况显示不同受击状态
/// 提供：位置信息 是否被攻击信息
/// </summary>
public class BodyHit : MonoBehaviour
{

    //是玩家阵营还是敌人阵营
    public bool isPlayer;
    Vector2 beatPos;
    Animator ani;
    Rigidbody2D rig;
    string hitClipName;
    bool isForward;
    float dir;
    [Header("观测:是否在地面/靠墙/重复攻击检测/是否是被攻击的状态")]
    public bool isGround;
    public bool isAgainstwall;
    public bool isAttacked;
    public bool beAttacking;


    AnimatorOverrideController overrideController;
    RuntimeAnimatorController tempController;

    [Header("动画片段-前向")]
    public AnimationClip hit_middle_forward_lightly;
    public AnimationClip hit_middle_forward_normal;
    public AnimationClip hit_middle_forward_heavily;

    public AnimationClip hit_middle_background_lightly;
    public AnimationClip hit_middle_background_normal;


    [Header("动画片段-向上")]
    public AnimationClip hit_up_forward_lightly;
    public AnimationClip hit_up_forward_normal;
    public AnimationClip hit_up_forward_heavily;

    [Header("动画片段-向下")]
    public AnimationClip hit_down_forward_lightly;
    public AnimationClip hit_down_forward_normal;
    public AnimationClip hit_down_forward_heavily;
    [Header("动画片段-颤抖")]
    public AnimationClip hit_shake;

    [Header("位置检测")]
    public Transform groundCheck;
    public Transform wallCheck;
    public float checkRadius;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        rig = this.GetComponent<Rigidbody2D>();
        tempController = ani.runtimeAnimatorController;
        overrideController = new AnimatorOverrideController();
        overrideController.runtimeAnimatorController = tempController;

        hitClipName = "hit_middle_forward_lightly";
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //同一角色0.2秒内只能受击一次
        if (!isAttacked)
        {
            
            //受击
            beatPos = other.ClosestPoint(transform.position);
            
            //判断自身位置
            dir = other.transform.position.x - transform.position.x;
            if ((dir < 0 && transform.right.x < 0) || (dir > 0 && transform.right.x > 0))
            {
                isForward = true;
            }
            else
            {
                isForward = false;
            }
            //拳击类
            if (other.CompareTag("Beat"))
            {
                //1受击
                Hitted(0);
                isAttacked = true;
                MonoMgr.GetInstance().StartCoroutine(IsAttackCo());
                //2生成效果
                PoolMgr.GetInstance().GetObj("beat", (o) => { o.transform.position = beatPos; });
                GlobalVolumeManager.instance.Shake();
                MusicMgr.GetInstance().PlaySound("BoxBeatNormal");
                //更新受击时间
                BeatManager.instance.attaacktime = Time.time;
                //3震动
                Cinemachine.CinemachineCollisionImpulseSource MyInpulse = this.GetComponent<Cinemachine.CinemachineCollisionImpulseSource>();
                if (MyInpulse != null)
                {
                    MyInpulse.GenerateImpulse();
                }

            }
            //轰击类的
            if (other.CompareTag("Bomb"))
            {
                //碰撞之后，令武器失效

            }
            //切割类的
            if (other.CompareTag("Sword"))
            {

            }
            //碾碎类的
            if (other.CompareTag("Hammer"))
            {

            }
            
           
        }
        
    }
    private void FixedUpdate()
    {
        PhysicsCheck();
    }
    private void Update()
    {
        //要在update获取
        if (ani.GetCurrentAnimatorStateInfo(2).IsName("hit"))//normalizedTime：0-1在播放、0开始、1结束 hitClipName为状态机动画的名字
        {
            //完成后的逻辑
            beAttacking = true;
        }
        else
        {
            beAttacking = false;
        }
    }
    void PhysicsCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);//圆检测
        isAgainstwall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, wallLayer);//圆检测

    }
    public void Hitted(int _otherDamage)
    {

        if (isGround)//陆地
        {
            if (isAgainstwall)
            {
                Debug.Log("贴着墙颤抖");
                PlayAnimation(hit_shake);
            }
            else
            {
                if (isForward)
                {
                    
                    Beat();
                }
                else
                {
                    switch (BeatManager.instance.dir)
                    {
                        case BeatDir.up:
                            Debug.Log("向前踉跄");
                            PlayAnimation(hit_middle_background_lightly);
                            rig.AddForce(new Vector2(BeatManager.instance.force, 0f), ForceMode2D.Impulse);
                            break;
                        case BeatDir.down:
                            Debug.Log("倒地");
                            PlayAnimation(hit_down_forward_heavily);
                            break;
                        case BeatDir.middle:
                            Debug.Log("向前踉跄");
                            PlayAnimation(hit_middle_background_normal);
                            rig.AddForce(new Vector2(BeatManager.instance.force, 0f), ForceMode2D.Impulse);
                            break;
                        case BeatDir.upM:
                            break;
                        case BeatDir.downM:
                            break;
                    }

                }
            }
        }
        else//空中
        {
            //空中靠墙受击
            if (isAgainstwall)
            {
                Debug.Log("贴着墙颤抖");
                PlayAnimation(hit_shake);
            }
            //空中受击
            else
            {
                switch (BeatManager.instance.dir)
                {
                    case BeatDir.up:
                        Debug.Log("空中向上击飞");
                        PlayAnimation(hit_up_forward_heavily);
                        rig.AddForce(new Vector2(0f,BeatManager.instance.force * 6), ForceMode2D.Impulse);
                        break;
                    case BeatDir.down:
                        Debug.Log("空中向下击倒");
                        PlayAnimation(hit_down_forward_heavily);
                        rig.AddForce(new Vector2(0f, -BeatManager.instance.force * 6), ForceMode2D.Impulse);
                        break;
                    case BeatDir.middle:
                        Debug.Log("空中直直击退");
                        PlayAnimation(hit_middle_forward_heavily);
                        rig.AddForce(new Vector2(BeatManager.instance.force * 6 * -transform.right.x, 0f), ForceMode2D.Impulse);
                        break;
                    case BeatDir.upM:
                        break;
                    case BeatDir.downM:
                        break;
                }
            }
        }
    }
    private void Beat()
    {
        switch (BeatManager.instance.dir)
        {
            case BeatDir.up:
                if (BeatManager.instance.force == 1)
                {
                    Debug.Log("一级向上击飞");
                    PlayAnimation(hit_up_forward_lightly);
                    rig.AddForce(new Vector2(0f, 1 * BeatManager.instance.force), ForceMode2D.Impulse);
                }
                else
                {
                    if (BeatManager.instance.force == 2)
                    {
                        Debug.Log("二级向上击飞");
                        PlayAnimation(hit_up_forward_normal);
                        rig.AddForce(new Vector2(0f, 2 * BeatManager.instance.force), ForceMode2D.Impulse);
                    }
                    else
                    {
                        Debug.Log("三级向上击飞");
                        PlayAnimation(hit_up_forward_heavily);
                        rig.AddForce(new Vector2(0f, 3 * BeatManager.instance.force), ForceMode2D.Impulse);
                    }
                }
                break;
            case BeatDir.down:
                if (BeatManager.instance.force == 1)
                {
                    Debug.Log("一级向下击倒");
                    PlayAnimation(hit_down_forward_lightly);
                }
                else
                {
                    if (BeatManager.instance.force == 2)
                    {
                        Debug.Log("二级向下击倒");
                        PlayAnimation(hit_down_forward_normal);
                    }
                    else
                    {
                        Debug.Log("三级向下击倒");
                        PlayAnimation(hit_down_forward_heavily);
                    }
                }
                break;
            case BeatDir.middle:
                if (BeatManager.instance.force == 1)
                {
                    Debug.Log("一级直直击退");
                    PlayAnimation(hit_middle_forward_lightly);
                    rig.AddForce(new Vector2( -1*transform.right.x * BeatManager.instance.force, 0f), ForceMode2D.Impulse);
                }
                else
                {
                    if (BeatManager.instance.force == 2)
                    {
                        Debug.Log("二级直直击退");
                        PlayAnimation(hit_middle_forward_normal);
                        rig.AddForce(new Vector2( -2 * transform.right.x * BeatManager.instance.force, 0f), ForceMode2D.Impulse);
                    }
                    else
                    {
                        Debug.Log("三级直直击退");
                        PlayAnimation(hit_middle_forward_heavily);
                        rig.AddForce(new Vector2(-3 * transform.right.x * BeatManager.instance.force, 0f), ForceMode2D.Impulse);
                    }
                }
                break;
            case BeatDir.upM:
                break;
            case BeatDir.downM:
                break;
        }
    }
    private void PlayAnimation(AnimationClip animationClip)
    {
        overrideController[hitClipName] = animationClip;
        ani.runtimeAnimatorController = overrideController;
        ani.SetTrigger("hit");
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        Gizmos.DrawWireSphere(wallCheck.position, checkRadius);
    }
    IEnumerator IsAttackCo()
    {
        yield return new WaitForSeconds(0.1f);
        isAttacked = false;
    }
}

