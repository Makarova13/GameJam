using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.UI;

namespace Assets.Scripts
{
    public sealed class TextTyper : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI textComponent;
        [SerializeField] float printDelay = 0.02f;
        [SerializeField] float punctuationDelay = 8f;

        private static readonly List<char> punctutationCharacters = new()
        {
            '.',
            ',',
            '!',
            '?'
        };

        private char[] charactersToType;
        private Coroutine typeTextCoroutine;
        private Action onFinish;
        
        public bool Typing { get; private set; }

        public void TypeText(string text, Action onFinish)
        {
            this.onFinish = onFinish;
            CleanupCoroutine();
            Typing = true;

            var textInfo = textComponent.textInfo;
            textInfo.ClearMeshInfo(false);

            typeTextCoroutine = StartCoroutine(TypeTextCharByChar(text));
        }

        public void Skip()
        {
            Typing = false;
            CleanupCoroutine();

            textComponent.maxVisibleCharacters = int.MaxValue;
            UpdateMeshAndAnims();
            onFinish?.Invoke();
        }

        private void CleanupCoroutine()
        {
            if (typeTextCoroutine != null)
            {
                StopCoroutine(typeTextCoroutine);
                typeTextCoroutine = null;
            }
        }

        private IEnumerator TypeTextCharByChar(string text)
        {
            charactersToType = text.ToCharArray();
            textComponent.text = text;

            for (int numPrintedCharacters = 0; numPrintedCharacters < charactersToType.Length; ++numPrintedCharacters)
            {
                if (!Typing) break;
                textComponent.maxVisibleCharacters = numPrintedCharacters + 1;
                UpdateMeshAndAnims();

                var printedChar = this.charactersToType[numPrintedCharacters];
                bool punctutation = punctutationCharacters.Contains(printedChar);

                yield return new WaitForSeconds(punctutation ? punctuationDelay : printDelay);
            }

            onFinish?.Invoke();
            Typing = false;
            typeTextCoroutine = null;
        }

        private void UpdateMeshAndAnims()
        {
            textComponent.ForceMeshUpdate();
        }
    }
}