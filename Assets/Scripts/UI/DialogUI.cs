using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SkeletonMistake
{
    public class DialogUI : MonoBehaviour
    {
        [SerializeField] private Text subtitle;

        private DialogData dialog;
        private int currentEntry = -1;

        void Start()
        {
            Events.OnDialogStart += DialogStart;
            Events.OnDialogEnd += DialogEnd;
            Events.OnDialogEntryChange += DialogEntryChange;
            Events.OnDialogSelected += DialogSelected;
            Events.OnPlayerTakeDamage += PlayerTakeDamage;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            Events.OnDialogStart -= DialogStart;
            Events.OnDialogEnd -= DialogEnd;
            Events.OnDialogSelected -= DialogSelected;
            Events.OnPlayerTakeDamage -= PlayerTakeDamage;
        }

        private void DialogStart(DialogData dialog)
        {
            gameObject.SetActive(true);
            this.dialog = dialog;

            Events.InvokeDialogEntryChange(dialog, 0);
        }

        private void DialogEnd(DialogData.DialogChoiceType result)
        {
            gameObject.SetActive(false);
            dialog = null;
            currentEntry = -1;
        }

        private void DialogEntryChange(DialogData dialog, int entryIndex)
        {
            if (entryIndex < 0 || entryIndex >= (dialog?.Entries?.Count ?? 0))
            {
                return;
            }

            var entry = dialog.Entries[entryIndex];
            subtitle.text = entry?.Subtitle ?? "";
            currentEntry = entryIndex;
        }

        private void DialogSelected(DialogData dialog, int choiceIndex)
        {
            if (currentEntry < 0 || currentEntry >= (dialog?.Entries?.Count ?? 0))
            {
                return;
            }

            var entry = dialog.Entries[currentEntry];
            if (choiceIndex < 0 || choiceIndex >= (entry?.Choices?.Count ?? 0))
            {
                return;
            }

            var choice = entry.Choices[choiceIndex];
            switch (choice.ChoiceType)
            {
                case DialogData.DialogChoiceType.NextDialog:
                    Events.InvokeDialogEntryChange(dialog, choice.NextDialogIndex);
                    break;
                case DialogData.DialogChoiceType.Success:
                    Events.InvokeDialogEnd(DialogData.DialogChoiceType.Success);
                    break;
                case DialogData.DialogChoiceType.Fail:
                    Events.InvokeDialogEnd(DialogData.DialogChoiceType.Fail);
                    break;
            }
        }

        private void PlayerTakeDamage(int health, int damage)
        {
            if (health <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

}