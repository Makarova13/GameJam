using UnityEngine;

[CreateAssetMenu(fileName = "NPC data", menuName = "Custom/Scriptable objects/NPC")]
public class NpcData : ScriptableObject
{
    [SerializeField] TextAsset chainedToWallDialogue;
    [SerializeField] TextAsset followingDialogue;
    [SerializeField] int health;

    public TextAsset ChainedToWallDialogue => chainedToWallDialogue;
    public TextAsset FollowingDialogue => followingDialogue;
    public int Health => health;
}
