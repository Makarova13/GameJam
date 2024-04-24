using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDeathState : NPCState
{
    public NPCDeathState(NpcController npc) : base(npc){}

    public override void OnEnter()
    {
        npc.DisableMovement();
        npc.gameObject.SetActive(false);    
    }
}
