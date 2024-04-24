using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLeftState : NPCState
{
    public NPCLeftState(NpcController npc) : base(npc){}

    public override void OnEnter()
    {
        npc.DisableMovement();
    }
    public override void OnExit() 
    {
        npc.EnableMovement();
    }
}
