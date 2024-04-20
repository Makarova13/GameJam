using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class DialogSystemController : MonoBehaviour
    {
        [SerializeField] Button _skipButton;
        [SerializeField] GameObject _panel;
        [SerializeField] TextTyper _dialogText;
        [SerializeField] List<ChoiceButton> _choiceButtons = new(3);

        public static DialogSystemController Instance;

        public UnityEvent<ChoiceType> PlayerResponded;
        public UnityEvent DialogEnded;

        private List<DialogWithChoices> _dialogs;
        private int _currentDialogIndex = 0;
        private bool _haveChoices;

        private void Awake()
        {
            Instance = this;

            for (int i = 0; i < _choiceButtons.Count; i++)
            {
                _choiceButtons[i].Subscribe(OnChoiceButtonClick);
            }
        }

        private void OnEnable()
        {
            _skipButton.onClick.AddListener(Skip);
        }

        private void OnDisable()
        {
            _skipButton.onClick.RemoveListener(Skip);
        }

        public void Init(List<DialogWithChoices> dialogs)
        {
            _dialogs = dialogs;
        }

        public void ShowCurrentDialog()
        {
            _panel.SetActive(true);

            ShowDialog(_dialogs[_currentDialogIndex]);
        }

        public void ShowNextDialog()
        {
            _currentDialogIndex++;

            if (_currentDialogIndex < _dialogs.Count)
                ShowDialog(_dialogs[_currentDialogIndex]);
            else
            {
                DialogEnded?.Invoke();

                gameObject.SetActive(false);
            }
        }

        public void ShowDialog(int index)
        {
            _currentDialogIndex = index;
            ShowDialog(_dialogs[index]);
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
            _dialogText.TypeText(dialog.Text, () => ShowButtons(dialog.Choices.Count));

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

        private void Skip()
        {
            if (_dialogText.Typing)
            {
                _dialogText.Skip();
            }
            else if(!_haveChoices)
            {
                ShowNextDialog();
            }
        }
    }
}
