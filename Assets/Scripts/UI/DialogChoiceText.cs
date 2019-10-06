using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SkeletonMistake
{
    public class DialogChoiceText : MonoBehaviour
    {
        [SerializeField] private int choiceIndex;

        private Text text;

        private void Start()
        {
            text = GetComponent<Text>();
            Events.OnDialogEntryChange += DialogEntryChange;
        }

        private void OnDestroy()
        {
            Events.OnDialogEntryChange -= DialogEntryChange;
        }

        private void DialogEntryChange(DialogData dialog, int entryIndex)
        {
            if(entryIndex < 0 || entryIndex >= (dialog?.Entries?.Count ?? 0))
            {
                return;
            }

            var entry = dialog.Entries[entryIndex];

            var choice = (choiceIndex >= 0 && choiceIndex < (entry?.Choices?.Count ?? 0)) ? entry.Choices[choiceIndex] : null;
            text.text = choice?.Text ?? "";
        }
    }
}
