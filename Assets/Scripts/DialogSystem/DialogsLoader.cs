﻿using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class DialogsLoader
    {
        public List<DialogWithChoices> LoadJsonData(string jsonName, bool random)
        {
            try
            {
                TextAsset jsonFile = Resources.Load<TextAsset>(jsonName);

                string jsonData = jsonFile.text;

                var dialogs = JsonUtility.FromJson<DialogsWithChoices>(jsonData).Dialogs;

                if (random)
                {
                    dialogs.Shuffle();
                }

                return JsonUtility.FromJson<DialogsWithChoices>(jsonData).Dialogs;
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error loading JSON data: " + e.Message);
                return null;
            }
        }
    }

    public static class ListExtansions
    {
        private static System.Random rng = new System.Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

    }
}