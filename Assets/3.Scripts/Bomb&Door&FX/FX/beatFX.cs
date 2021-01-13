using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beatFX : MonoBehaviour
{
    private void OnEnable()
    {
        this.GetComponent<Animator>().SetTrigger("enable");

        StartCoroutine(PushObj());
    }
    private IEnumerator PushObj()
    {
        yield return new WaitForSeconds(1f);
        PoolMgr.GetInstance().PushObj(PathCfg.PATH_FX + this.name, this.gameObject);
    }


}
