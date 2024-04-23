using UnityEngine;

[CreateAssetMenu(fileName = "NPC data", menuName = "Custom/Scriptable objects/NPC")]
public class NpcData : ScriptableObject
{
    [SerializeField] TextAsset chainedToWallDialogue;
    [SerializeField] int health;

    public TextAsset ChainedToWallDialogue => chainedToWallDialogue;
    public int Health => health;
}
