using Assets.Scripts;
using UnityEngine;

public class NpcInteractibleController : BaseInteractibleObject
{
    [SerializeField] NpcController controller;

    public override void Interact()
    {
        DialogSystemController.Instance.Init(DialogsLoader.GetJsonData(controller.Data.ChainedToWallDialogue));
    }
}
