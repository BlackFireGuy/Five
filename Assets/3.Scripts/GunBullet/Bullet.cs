using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float flySpeed;//飞行速度
    public float arrawDistance;
    public Vector3 startPos;
    public string bulletPath = "Prefabs/Bomb&TVAndSoOn/GUN&Bullet/Bullet001";

    public GameObject damageCanvas;
    protected virtual void Start()
    {

    }
    private void Update()
    {
        float distance = (transform.position - startPos).sqrMagnitude;

        
        if (distance > arrawDistance)
        {
            PoolMgr.GetInstance().PushObj(bulletPath, this.gameObject);
        }
    }

    private void OnEnable()
    {
        startPos = FindObjectOfType<PlayerController>().transform.position;
    }
    public void SetStartPos(Vector3 pos)
    {
        startPos = pos;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.CompareTag("Enemy"))
        {

            //this.GetComponent<Animator>().Play("Bullets",0);
            other.GetComponent<IDamageable>().GetHit(PlayerInfoManager.instance.info.attack);
            if (damageCanvas != null)
            {
                DamageNum dg = Instantiate(damageCanvas, other.transform.position, Quaternion.identity).GetComponent<DamageNum>();
                dg.ShowUIDamage(PlayerInfoManager.instance.info.attack);
            }
            else
            {
                Debug.Log("damageCanvas为空");
            }
            //震动
            Cinemachine.CinemachineCollisionImpulseSource MyInpulse = other.GetComponent<Cinemachine.CinemachineCollisionImpulseSource>();
            if (MyInpulse != null)
            {
                MyInpulse.GenerateImpulse();
            }
            else
            {
                Debug.Log("MyInpulse为空！");
            }

                
            //同时击退
            //this.transform.parent.GetComponent<Enemy>().hitDialog.SetActive()
            Vector3 p2 = (other.transform.position - transform.position).normalized;

            other.transform.position = new Vector2(
                other.transform.position.x + p2.x,
                other.transform.position.y + p2.y);

            PoolMgr.GetInstance().PushObj(bulletPath, this.gameObject);
            

        }


        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>().GetHit(damage);
            PoolMgr.GetInstance().PushObj(bulletPath, this.gameObject);

        }
        /* if (collision.gameObject.CompareTag("Player"))
         {
             
             collision.GetComponent<IDamageable>().GetHit(damage);
         }*/
    }


    
}

