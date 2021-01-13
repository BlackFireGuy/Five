using UnityEngine;
/// <summary>
/// 通过鼠标控制和AI控制镜头的旋转和远近高低
/// </summary>
public class CameraController : MonoBehaviour
{
    //角色的transform
    Transform target;

    //平滑缓冲的程度
    public float dampTrace = 20.0f;//摄像机跟随的移动速度
    //偏移量
    Vector3 offset = new Vector3(0,0,-10);

    private void Update()
    {
        target = FindObjectOfType<PlayerController>().transform;
    }

    private void LateUpdate()
    {
        if (target == null) { return; }
        transform.position = Vector3.Lerp(transform.position, target.position + offset, dampTrace * Time.deltaTime);

    }
}
