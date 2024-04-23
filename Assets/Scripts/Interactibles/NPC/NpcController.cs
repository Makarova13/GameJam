using UnityEngine;

public class NpcController : MonoBehaviour
{
    [SerializeField] NpcData data;
    [SerializeField] NPCAnimationController animationController;

    public NpcData Data => data;
}
