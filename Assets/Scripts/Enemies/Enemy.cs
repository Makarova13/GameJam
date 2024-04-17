using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private float agroDistance;
    [SerializeField] private float attackRange;
    [SerializeField] private float viewAngle;
    [SerializeField] private LayerMask layerMask;
    private Vector3 startPosition;
    private EnemyState currentState;
    
    private void Awake()
    {
        startPosition = transform.position;
        TransitionToState(new IdleState(this));
    }
    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.blue,5f);
        currentState.Execute();
    }

    public void TransitionToState(EnemyState enemyState)
    {
        currentState?.OnExit();
        enemyState.OnEnter();
        currentState = enemyState;
    }

    public void ChasePlayer()
    {
        agent.SetDestination(player.position);  
    }

    public void GoToStartPosition()
    {
        agent.SetDestination(startPosition);
    }

    public bool TryToFindPlayer()
    {
        if(agroDistance >= Vector3.Distance(transform.position, player.position))
            return true;

        return IsSeePlayer(); 
    }

    public bool IsInAttackRange()
    {       
        return attackRange >= Vector3.Distance(transform.position, player.position);
    }

    public bool IsSeePlayer()
    {
        var direction = player.position - transform.position;
        if ( viewAngle >= Vector3.Angle(transform.forward, direction))
        {        
            if(Physics.Raycast(transform.position, player.position - transform.position, out RaycastHit hitInfo, Mathf.Infinity, layerMask))
            {
                return hitInfo.collider.gameObject.tag == "Player";
            }

        }
        return false;
    } 
    
}
