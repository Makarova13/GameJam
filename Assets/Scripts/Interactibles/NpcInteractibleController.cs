using Assets.Scripts;
using UnityEngine;

public class NpcInteractibleController : BaseInteractibleObject
{
    [SerializeField] NpcController controller;

    private void Start()
    {
        DialogSystemController.Instance.PlayerResponded.AddListener((r) =>
        {
            if (r == ChoiceType.Right)
            {
                controller.StartFollowing();
            }
        });
    }
    public override void Interact()
    {
        DialogSystemController.Instance.Init(DialogsLoader.GetJsonData(controller.Data.ChainedToWallDialogue));
        DialogSystemController.Instance.ShowCurrentDialog();
    }
}
