
public class DeathState : EnemyState
{
    public DeathState(Enemy e) : base(e){}
    public override void OnEnter()
    {
        enemy.DisableMovement();
        enemy.gameObject.SetActive(false);
    }
}
