
public class ChaseState : EnemyState
{
    public ChaseState(Enemy e) : base(e) { }

    public override void Execute()
    {
        if (enemy.IsInAttackRange())
            enemy.TransitionToState(new AttackState(enemy));

        else if (enemy.TryToFindPlayer())
            enemy.ChasePlayer();

        else
            enemy.TransitionToState(new IdleState(enemy));
    }
}
