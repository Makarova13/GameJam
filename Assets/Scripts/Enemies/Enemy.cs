using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.Progress;

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
    private Transform target;
    private Vector3 startPosition;
    private EnemyState currentState;
    private Vector3 facing; 

    private void Start()
    {
        SlowDown(); // :D
        FlashLightController.OnStartLighting += SlowDown;
        FlashLightController.OnStoptLighting += SpeedUp;

        target = Player.instance.gameObject.transform;
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

    public void ChaseTarget()
    {
        agent.SetDestination(target.position);
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
    public bool TryToFindTarget()
    {
        for (int i = Player.instance.TargetsForEnemy.Count - 1; i >= 0; i--)
        {
            var item = Player.instance.TargetsForEnemy[i];
            if (agroDistance >= Vector3.Distance(transform.position, item.position) || IsSeeTarget(item.position))
            {
                target = item;
                return true;
            }
        }
        return false;
    }

    public bool IsInAttackRange()
    {
        for (int i = Player.instance.TargetsForEnemy.Count - 1; i >= 0; i--)
        {
            var item = Player.instance.TargetsForEnemy[i];
            if (attackRange >= Vector3.Distance(transform.position, item.position) && IsSeeTarget(item.position))
                return true;
        }
        return false;
    }

    private bool IsSeeTarget(Vector3 target)
    {
        var direction = target - transform.position;
        if ( viewAngle >= Vector3.Angle(facing, direction))
        {        
            Debug.DrawRay(transform.position, target - transform.position, Color.red, Mathf.Infinity);
            if(Physics.Raycast(transform.position, target - transform.position, out RaycastHit hitInfo, Mathf.Infinity, layerMask))
            {
                return hitInfo.collider.gameObject == Player.instance.gameObject || hitInfo.collider.gameObject.tag == "NPC";
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
public static class NavMeshAgentHelper
{
    public static Vector3 GetFacing(this NavMeshAgent agent)
    {
        var movementDirection = agent.velocity;
        if (movementDirection.x > 0 && movementDirection.x >= Mathf.Abs(movementDirection.z))
        {           
            //facing right
            return agent.transform.right;
        }
        else if (movementDirection.x < 0 && Mathf.Abs(movementDirection.x) >= Mathf.Abs(movementDirection.z))
        {
            //facing left
            return agent.transform.right * -1;
        }      
        else if(movementDirection.z > 0)
        {
            //facing up
            return agent.transform.forward;
        }
        else
        {
            //facing down
            return agent.transform.forward * -1;
        }
    }
}
