using DG.Tweening;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager instance;
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    [Header("相机设置")]
    /*public Animator ani;
    public Camera cam;*/
    public Transform camPos;
    public float duration;
    public float strength; 
    public void ShakeCamera()
    {
        //cam.DOShakePosition(duration,strength);
        //ani.Play("Shake");
        //ani.SetTrigger("shake");

        camPos.position = new Vector3(camPos.position.x + strength, camPos.position.y + strength ,camPos.position.z);
    }
}
