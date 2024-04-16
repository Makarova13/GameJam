
public class ChaseState : EnemyState
{    
    public ChaseState(Enemy e) : base(e) { }

    public override void Execute()
    {
        if (enemy.IsInAttackRange())
        {
            enemy.TransitionToState(new AttackState(enemy));
            return;
        }
        enemy.SniffSniff();
        enemy.SetDestination();       
    }
}
