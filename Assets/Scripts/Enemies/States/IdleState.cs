
public class IdleState : EnemyState
{
    public IdleState(Enemy e) : base(e){}

    public override void OnEnter()
    {
        enemy.GoToStartPosition();
    }
    public override void Execute()
    {
        if (enemy.TryToFindPlayer()) 
        {
            enemy.TransitionToState(new ChaseState(enemy));
        }
        if (enemy.HasPath)
            return;
        if (!enemy.IsDestenationStartPosition())
        {
            enemy.GoToStartPosition();
        }
        else
        {
            enemy.GoToSetPathPoint();
        }
    }
}
