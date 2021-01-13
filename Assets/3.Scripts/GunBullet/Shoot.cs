using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 用到了数据池
/// </summary>
public class Shoot : MonoBehaviour
{
    //public GameObject bullet;
    public Transform muzzleTransform;
    [Header("攻击设置")]
    public float attackSpeed;//攻击频率
    public float currentTime = 0;
    bool isStart;
    Joystick joystick;
    [Header("装备设置")]
    //public bool isEquip = false;
    public string bulletPath = "Prefabs/Bomb&TVAndSoOn/GUN&Bullet/Bullet001";

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {

        if(joystick == null)
        {
            joystick = FindObjectOfType<FixedJoystick>().GetComponent<FixedJoystick>();
            return;
        }
        else
        {
            float angle = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;
            float horizontalInput = joystick.Horizontal;
            if (horizontalInput > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, angle);
            }
            if (horizontalInput < 0)
            {
                transform.eulerAngles = new Vector3(0, 0, angle);
            }

            if (Mathf.Abs(joystick.Vertical) > 0 || Mathf.Abs(joystick.Horizontal) > 0)
            {
                if (!isStart)
                {
                    PoolMgr.GetInstance().GetObj(bulletPath, InitBullet);
                    isStart = true;
                    currentTime = Time.time;
                }
                if (currentTime > 0 && attackSpeed > 0 && isStart == true && Time.time - currentTime > attackSpeed)
                {
                    isStart = false;
                }
            }
        }

        
    }

    private void InitBullet(GameObject arg0)
    {
        arg0.transform.position = muzzleTransform.position;
        arg0.transform.rotation = Quaternion.Euler(transform.eulerAngles);
        arg0.GetComponent<Rigidbody2D>().velocity = (new Vector2(joystick.Horizontal, joystick.Vertical)).normalized * arg0.GetComponent<Bullet>().flySpeed;

        
    }
}
