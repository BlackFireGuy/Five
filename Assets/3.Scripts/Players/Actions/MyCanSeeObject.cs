using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
/// <summary>
/// 用来判断目标是否在视野内
/// </summary>
public class MyCanSeeObject : Conditional
{
    public Transform[] targets;//判断是否在视野内的目标

    public SharedTransform currentTarget;
    //public float viewDistance = 7;
    public SharedFloat sharedViewDistance  =7;
    public float fieldOfViewAngle = 90;

    public override TaskStatus OnUpdate()
    {
        if (targets == null) return TaskStatus.Failure;
        foreach (var target in targets)
        {
            float distance = (target.position - transform.position).magnitude;
            float angle = Vector3.Angle(transform.right, target.position - transform.position);
            if(distance < sharedViewDistance.Value&& angle<fieldOfViewAngle*05f)
            {
                this.currentTarget.Value = target;
                return TaskStatus.Success;
            }
        }

        return TaskStatus.Running;
    }
}
