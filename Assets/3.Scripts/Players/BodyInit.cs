using UnityEngine;

public class BodyInit : MonoBehaviour
{
    
    /// <summary>
    /// 初始化body伤害响应
    /// </summary>
    void Start()
    {
        foreach (Transform item in transform)
        {

            Debug.Log(item.name);
            /*BodyHit bodyHit =  item.gameObject.AddComponent<BodyHit>();
            if (item.gameObject.CompareTag("Beat"))
            {
                bodyHit.isArmour = true;
            }*/
        }
    }
}
