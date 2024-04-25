using Assets.Scripts;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class NpcController : MonoBehaviour
{
    [SerializeField] NpcData data;
    [SerializeField] NPCAnimationController animationController;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Health health;
    [SerializeField] private float stopNearPlayer = 3.5f;
    [SerializeField] private GameObject chains;
    private NPCState currentState;
    private Transform player;

    public NpcData Data => data;
    public NPCAnimationController AnimationController => animationController;
    public bool IsTaken { get; private set; }

    private void Start()
    {
        health.OnDeath += () => Player.instance.TargetsForEnemy.Remove(transform);
        health.OnDeath += () => TransitionToState(new NPCDeathState(this));

        player = Player.instance.transform;
        TransitionToState(new NPCLeftState(this));

    }
    private void Update()
    {

        currentState.Execute();
    }

    public void Free()
    {
        chains.SetActive(false);
    }

    public void TransitionToState(NPCState state)
    {
        currentState?.OnExit();
        currentState = state;
        currentState.OnEnter();
    }

    public void FollowPlayer()
    {
        agent.SetDestination(player.position);
        PlayWalkAnimation();
    }
    public bool IsPlayerNear()
    {
        return Vector3.Distance(player.position, transform.position) < stopNearPlayer;
    }
    public void DisableMovement()
    {
        agent.destination = transform.position;
        agent.isStopped = true;
    }
    public void EnableMovement()
    {
        agent.isStopped = false;
    }
    public void StartFollowing()
    {
        TransitionToState(new NPCFollowState(this));
        Player.instance.TargetsForEnemy.Add(transform);
        IsTaken= true;
    }
    private void PlayWalkAnimation()
    {
        animationController.PlayWalkButWorse(agent.GetFacing());
    }
}
