using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class CutsceneController : MonoBehaviour
    {
        [SerializeField] List<CutsceneFrame> frames;
        [SerializeField] string nextScene;
        [SerializeField] InputActionReference skipAction;

        private int currentFrameIndex = 0;
        private float elapsedTime = 0.0f;

        private void OnEnable()
        {
            skipAction.action.performed += OnSkipAction;
        }

        private void Update()
        {
            if (currentFrameIndex < frames.Count)
            {
                elapsedTime += Time.deltaTime;

                if (elapsedTime >= frames[currentFrameIndex].ShowNextTime)
                {
                    elapsedTime = 0;
                    NextFrame();
                }
            }
        }

        private void OnDisable()
        {
            skipAction.action.performed -= OnSkipAction;
        }

        private void OnSkipAction(InputAction.CallbackContext _)
        {
            NextFrame();
        }

        private void NextFrame()
        {
            frames[currentFrameIndex].ShowImidietly();

            elapsedTime = 0;
            currentFrameIndex++;

            if (currentFrameIndex < frames.Count)
                frames[currentFrameIndex].Show();
            else
                SkipAll();
        }

        public void SkipAll()
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
