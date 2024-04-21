using Assets.Scripts;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public bool HasPath => agent.hasPath;
    public SimpleEnemyAttack Attack;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float speedInDark;
    [SerializeField] private float speedOnLight;
    [SerializeField] private float agroDistance;
    [SerializeField] private float attackRange;
    [SerializeField] private float viewAngle;
    [SerializeField] private Health health;
    [SerializeField] private Transform setPathPoint;
    [SerializeField] private LayerMask layerMask;
    private Transform player;
    private Vector3 startPosition;
    private EnemyState currentState;
    private Vector3 facing; 

    private void Start()
    {
        SlowDown(); // :D
        FlashLightController.OnStartLighting += SlowDown;
        FlashLightController.OnStoptLighting += SpeedUp;

        player = Player.instance.gameObject.transform;
        health.OnDeath += () => TransitionToState(new DeathState(this));
        startPosition = transform.position;
        TransitionToState(new IdleState(this));
        facing = transform.forward * -1;
    }
    private void Update()
    {
        GetFacing();
        Attack.transform.position = transform.position + facing;     
        currentState.Execute();
    }
    private void OnDestroy()
    {
        FlashLightController.OnStartLighting -= SlowDown;
        FlashLightController.OnStoptLighting -= SpeedUp;
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
        startPosition = agent.destination;
    }
    public void GoToSetPathPoint()
    {
        agent.SetDestination(setPathPoint.position);
    }
    public bool IsDestenationStartPosition() => agent.destination == startPosition; 

    public void DisableMovement()
    {
        agent.destination = transform.position; 
        agent.isStopped = true;
    }
    public void EnableMovement()
    {
        agent.isStopped = false;
    }
    public bool TryToFindPlayer()
    {
        if(agroDistance >= Vector3.Distance(transform.position, player.position))
            return true;

        return IsSeePlayer(); 
    }

    public bool IsInAttackRange()
    {
        return attackRange >= Vector3.Distance(transform.position, player.position) && IsSeePlayer();
    }

    public bool IsSeePlayer()
    {
        var direction = player.position - transform.position;
        if ( viewAngle >= Vector3.Angle(facing, direction))
        {        
            Debug.DrawRay(transform.position, player.position - transform.position, Color.red, Mathf.Infinity);
            if(Physics.Raycast(transform.position, player.position - transform.position, out RaycastHit hitInfo, Mathf.Infinity, layerMask))
            {
                return hitInfo.collider.gameObject == Player.instance.gameObject;
            }

        }
        return false;
    }    
    private void GetFacing()
    {
        var movementDirection = agent.velocity;
        if (movementDirection.x > 0 && movementDirection.x >= Mathf.Abs(movementDirection.z))
        {
            //facing right
            facing = transform.right;
        }
        else if (movementDirection.x < 0 && Mathf.Abs(movementDirection.x) >= Mathf.Abs(movementDirection.z))
        {
            //facing left
            facing = transform.right * -1;
        }
        else if (movementDirection.z > 0)
        {
            // facing up
            facing = transform.forward;

        }
        else if (movementDirection.z < 0)
        {
            //facing down
            facing = transform.forward * -1;            
        }
        Debug.DrawRay(transform.position, facing, Color.blue, 5f);
    }
    private void SpeedUp()
    {
        agent.speed = speedInDark;
    }
    private void SlowDown()
    {
        agent.speed = speedOnLight;
        
    }
}
