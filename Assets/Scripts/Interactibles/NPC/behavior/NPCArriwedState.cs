using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCArriwedState : NPCState
{
    public NPCArriwedState(NpcController npc) : base(npc){}

    public override void OnEnter() => npc.DisableMovement();

    public override void Execute()
    {
        if (npc.IsPlayerNear() == false)
            npc.TransitionToState(new NPCFollowState(npc));
    }
    public override void OnExit() => npc.EnableMovement();
}
