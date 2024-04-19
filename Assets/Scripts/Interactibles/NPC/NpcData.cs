using UnityEngine;

[CreateAssetMenu(fileName = "NPC data", menuName = "Custom/Scriptable objects/NPC")]
public class NpcData : ScriptableObject
{
    [SerializeField] TextAsset chainedToWallDialogue;

    public TextAsset ChainedToWallDialogue => chainedToWallDialogue;
}
