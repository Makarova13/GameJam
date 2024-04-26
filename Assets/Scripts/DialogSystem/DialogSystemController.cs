using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class DialogSystemController : MonoBehaviour
    {
        [SerializeField] Button _skipButton;
        [SerializeField] InputActionReference skipAction;
        [SerializeField] GameObject _panel;
        [SerializeField] TextTyper _dialogText;
        [SerializeField] List<ChoiceButton> _choiceButtons = new(3);
        [SerializeField] TextAsset _openingDialogue;
        [SerializeField] bool _showOpeningDialogue;
        [SerializeField] Player player;
        [SerializeField] AudioSource phoneSource;

        public static DialogSystemController Instance;

        public UnityEvent<ChoiceType> PlayerResponded;
        public UnityEvent DialogStarted;
        public UnityEvent DialogEnded;
        public bool IsTyping;

        private List<DialogWithChoices> _dialogs;
        private int _currentDialogIndex = 0;
        private bool _haveChoices;

        public bool isActive = false;

        private void Awake()
        {
            Instance = this;

            for (int i = 0; i < _choiceButtons.Count; i++)
            {
                _choiceButtons[i].Subscribe(OnChoiceButtonClick);
            }

            if (_showOpeningDialogue && _openingDialogue != null)
            {
                StartCoroutine(WaitAndStart());
            }

            skipAction.action.performed += OnSkipAction;
            _skipButton.onClick.AddListener(Skip);
        }

        public void Init(List<DialogWithChoices> dialogs)
        {
            _dialogs = dialogs;
            _currentDialogIndex = 0;
        }

        public void ShowCurrentDialog()
        {
            _panel.SetActive(true);
            isActive = true;

            ShowDialog(_dialogs[_currentDialogIndex % _dialogs.Count]);
        }

        public void ShowNextDialog()
        {
            _currentDialogIndex++;

            if (_currentDialogIndex < _dialogs.Count)
                ShowDialog(_dialogs[_currentDialogIndex]);
            else
            {
                _panel.gameObject.SetActive(false);
                isActive = false;
            }
        }

        public void ShowDialog(int index)
        {
            _currentDialogIndex = index;
            ShowDialog(_dialogs[index]);
        }

        private IEnumerator WaitAndStart()
        {
            phoneSource.Play();

            yield return new WaitForSeconds(4f);

            phoneSource.Pause();

            Init(DialogsLoader.GetJsonData(_openingDialogue));
            ShowCurrentDialog();
            StartCoroutine(FirstDialogue());
        }

        private IEnumerator FirstDialogue()
        {
            while (_panel.gameObject.activeSelf)
            {
                player.AnimateCalling();

                yield return null;
            }

            yield break;
        }

        private void OnChoiceButtonClick(Choice choice)
        {
            ShowButtons(0);
            _haveChoices = false;

            if (!string.IsNullOrEmpty(choice.Response))
            {
                _dialogText.TypeText(choice.Response, () =>
                {
                    PlayerResponded?.Invoke(choice.ChoiceType);
                });
            }
            else
            {
                PlayerResponded?.Invoke(choice.ChoiceType);
                ShowNextDialog();
            }
        }

        private void ShowDialog(DialogWithChoices dialog)
        {
            ShowButtons(0);
            _haveChoices = dialog.Choices != null && dialog.Choices.Count > 0;
            _dialogText.TypeText(dialog.Text, () =>
            {
                ShowButtons(dialog.Choices.Count);
                IsTyping = false;

                DialogEnded?.Invoke();
            });

            DialogStarted.Invoke();
            IsTyping = true;

            for (int i = 0; i < dialog.Choices.Count; i++)
            {
                _choiceButtons[i].Init(dialog.Choices[i]);
            }
        }

        private void ShowButtons(int number)
        {
            foreach (var button in _choiceButtons)
            {
                button.gameObject.SetActive(false);
            }

            for (int i = 0; i < number; i++)
            {
                _choiceButtons[i].gameObject.SetActive(true);
            }
        }

        private void OnSkipAction(InputAction.CallbackContext _)
        {
            Skip();
        }

        private void Skip()
        {
            if (_panel.gameObject.activeSelf)
            {
                if (_dialogText.Typing)
                {
                    _dialogText.Skip();
                }
                else if (!_haveChoices)
                {
                    ShowNextDialog();
                }
            }
        }
    }
}
