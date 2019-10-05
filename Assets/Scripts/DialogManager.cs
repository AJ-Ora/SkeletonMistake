using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkeletonMistake
{
    public class DialogManager : MonoBehaviour
    {
        public static DialogManager Instance { get; private set; }

        public DialogData Data;

        public DialogData.DialogEntry GetEntry(int index)
        {
            return index >= 0 && index < Data.Entries.Count ? Data.Entries[index] : null;
        }

        public DialogData.DialogChoice GetChoice(DialogData.DialogEntry entry, int index)
        {
            return index >= 0 && index < entry.Choices.Count ? entry.Choices[index] : null;
        }

        private void Start()
        {
            Instance = this;
        }
    }
}