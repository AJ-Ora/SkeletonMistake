using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SkeletonMistake
{
    public class DialogSelectionButton : MonoBehaviour
    {
        [SerializeField] private int choiceIndex;

        private Button button;

        private DialogData dialog;

        private void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
            Events.OnDialogEntryChange += DialogEntryChange;
        }

        private void OnDestroy()
        {
            button?.onClick.RemoveListener(OnClick);
            Events.OnDialogEntryChange -= DialogEntryChange;
        }

        private void OnClick()
        {
            Events.InvokeDialogSelected(dialog, choiceIndex);
        }

        private void DialogEntryChange(DialogData dialog, int entryIndex)
        {
            if (entryIndex < 0 || entryIndex >= (dialog?.Entries?.Count ?? 0))
            {
                return;
            }

            var entry = dialog.Entries[entryIndex];
            if (choiceIndex < 0 || choiceIndex >= (entry?.Choices?.Count ?? 0))
            {
                return;
            }

            var choice = entry.Choices[choiceIndex];
            button.enabled = choice != null;
            this.dialog = dialog;
        }
    }
}
