using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ChoiceButton : MonoBehaviour
    {
        [SerializeField] Button _button;
        [SerializeField] TMP_Text _text;

        private Choice _choice;

        public void Init(Choice choice)
        {
            _choice = choice;
            _text.text = choice.Text;
        }

        public void Subscribe(Action<Choice> onClicked)
        {
            _button.onClick.AddListener(() => onClicked.Invoke(_choice));
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}
