using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    [Serializable]
    public class DialogsWithChoices
    {
        public List<DialogWithChoices> Dialogs;
    }

    [Serializable]
    public class DialogWithChoices
    {
        public string Text;
        public List<Choice> Choices;
    }

    [Serializable]
    public class Choice
    {
        public string Text;
        public ChoiceType ChoiceType;
        public string Response;
    }

    public enum ChoiceType
    {
        Neutrual = -1,
        Wrong = 0,
        Right = 1,
    }
}
