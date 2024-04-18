
using Unity.VisualScripting;

public class AttackState : EnemyState
{
    private SimpleEnemyAttack simpleattack;

    public AttackState(Enemy enemy) : base(enemy)
    {
        simpleattack = enemy.Attack;
    }

    public override void OnEnter()
    {
        enemy.DisableMovement();
    }
    public override void OnExit()
    {
        enemy.EnableMovement();
    }

    public override void Execute()
    {
        if (simpleattack.IsReady() && enemy.IsInAttackRange())
            simpleattack.Attack();
        else
            enemy.TransitionToState(new ChaseState(enemy));
    }
}
