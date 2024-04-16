using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private float agroDistance;
    [SerializeField] private float attackRange;    
    private Vector3 destination; 
    private EnemyState currentState;
    
    private void Awake()
    {
        currentState = new IdleState(this);
    }
    private void Update()
    {
        currentState.Execute();
    }

    public void TransitionToState(EnemyState enemyState)
    {
        currentState.OnExit();
        enemyState.OnEnter();
        currentState = enemyState;
    }
    public void SetDestination()
    {
        agent.SetDestination(destination);  
    }

    public bool SniffSniff()
    {
        if(agroDistance >= Vector3.Distance(transform.position, player.position))
        {
            destination = player.position;
            return true;
        }
        return false;    
    }
    public bool IsInAttackRange()
    {       
        return attackRange >= Vector3.Distance(transform.position, player.position);
    }
}
