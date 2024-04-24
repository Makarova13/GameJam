using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollowState : NPCState
{
    public NPCFollowState(NpcController npc) : base(npc){}

    public override void Execute()
    {
        if(npc.IsPlayerNear())
        {
            npc.TransitionToState(new NPCArriwedState(npc));
        }
        npc.FollowPlayer();
    }
}
