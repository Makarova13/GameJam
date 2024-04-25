using Assets.Scripts;
using System.Collections;
using UnityEngine;

public class NpcInteractibleController : BaseInteractibleObject
{
    [SerializeField] NpcController controller;

    private void Start()
    {
        DialogSystemController.Instance.PlayerResponded.AddListener((r) =>
        {
            if (!controller.IsTaken)
                StartCoroutine(FirstResponseCoroutine(r));
        });

        DialogSystemController.Instance.DialogStarted.AddListener(() =>
        {
            controller.AnimationController.PlayIdleTalking();
        });

        DialogSystemController.Instance.DialogEnded.AddListener(() =>
        {
            controller.AnimationController.PlayIdle();
        });
    }

    public override void Interact()
    {
        if (!controller.IsTaken)
        {
            DialogSystemController.Instance.Init(DialogsLoader.GetJsonData(controller.Data.ChainedToWallDialogue));
        }
        else
        {
            DialogSystemController.Instance.Init(DialogsLoader.GetJsonData(controller.Data.FollowingDialogue));
        }

        DialogSystemController.Instance.ShowCurrentDialog();
    }

    IEnumerator FirstResponseCoroutine(ChoiceType choiceType)
    {
        while (DialogSystemController.Instance.IsTyping)
        {
            controller.AnimationController.PlayIdleTalking();

            yield return null;
        }

        if (choiceType == ChoiceType.Wrong)
        {
            controller.AnimationController.PlayIdleScared();
        }
        else
        {
            controller.Free();
            controller.AnimationController.PlayIdleHappy();
        }

        yield return new WaitForSeconds(2);

        if (choiceType == ChoiceType.Right)
        {
            controller.StartFollowing();
        }

        yield break;
    }
}
