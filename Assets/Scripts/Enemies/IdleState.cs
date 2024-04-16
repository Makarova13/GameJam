
public class IdleState : EnemyState
{
    public IdleState(Enemy e) : base(e){}

    public override void Execute()
    {
        if (enemy.SniffSniff()) 
        {
            enemy.TransitionToState(new ChaseState(enemy));
        }
    }
}
