
public class AttackState : EnemyState
{
    public AttackState(Enemy enemy) : base(enemy) { }

    public override void Execute()
    {
        if (!enemy.IsInAttackRange()) 
            enemy.TransitionToState(new ChaseState(enemy));

        //else if(check cd)   attack
    }
}
