/// <summary>
/// 抽象类，只声明方法，子类去实现方法
/// </summary>
public abstract class EnemyBaseState
{
    public abstract void EnterState(Enemy enemy);

    public abstract void OnUpdate(Enemy enemy);
}
