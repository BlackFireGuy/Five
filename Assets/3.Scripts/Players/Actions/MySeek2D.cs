using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

/// <summary>
/// 控制游戏物体到达目标位置
/// </summary>
public class MySeek2D : Action//这个任务的调用是由behaviordesigner行为树控制的
{
    public float speed;
    public SharedTransform target;//要到达的目标位置
    public float arrivedDistance = 0.1f;
    private float sqlArrivedDis;
    public override void OnStart()
    {
        sqlArrivedDis = arrivedDistance * arrivedDistance;
    }

    public override TaskStatus OnUpdate()//当进入到这个任务的时候，会一直调用这个方法，一直到任务结束，返回一个成功或者失败的状态 那么任务结束 如果返回一个Running的状态，那这个方法会继续调用 这个方法的调用频率，默认跟Unity里面的帧保持一致
    {
        if (target == null||target.Value == null) return TaskStatus.Failure;

        transform.LookAt(target.Value.position);//直接朝向目标位置
        transform.position = Vector2.MoveTowards(transform.position, target.Value.position, speed * Time.deltaTime);
        FilpDirection();
        if ((target.Value.position - transform.position).sqrMagnitude < sqlArrivedDis){
            return TaskStatus.Success;//如果距离目标位置的距离比较小，认为达到了目标位置，直接return成功
        }
        return TaskStatus.Running;
    }
    public void FilpDirection()
    {
        if (transform.position.x < target.Value.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

    }
}
