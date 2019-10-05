using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SkeletonMistake
{
    public class DialogUI : MonoBehaviour
    {
        [SerializeField] private Text subtitle;

        private int currentDialog = -1;

        void Start()
        {
            Events.OnDialogStart += DialogStart;
            Events.OnDialogSelected += DialogSelected;
            Events.OnDialogEnd += DialogEnd;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            Events.OnDialogStart -= DialogStart;
            Events.OnDialogSelected -= DialogSelected;
            Events.OnDialogEnd -= DialogEnd;
        }

        private void DialogStart(int dialogIndex)
        {
            gameObject.SetActive(true);
            currentDialog = dialogIndex;

            var entry = DialogManager.Instance.GetEntry(currentDialog);
            subtitle.text = entry?.Subtitle;
        }

        private void DialogSelected(int choiceIndex)
        {
            var entry = DialogManager.Instance.GetEntry(currentDialog);
            if(entry == null)
            {
                return;
            }

            var choice = DialogManager.Instance.GetChoice(entry, choiceIndex);
            if(choice == null)
            {
                return;
            }

            switch (choice.ChoiceType)
            {
                case DialogData.DialogChoiceType.NextDialog:
                    Events.InvokeDialogStart(choice.NextDialogIndex);
                    break;
                case DialogData.DialogChoiceType.Success:
                    Events.InvokeDialogEnd(DialogData.DialogChoiceType.Success);
                    break;
                case DialogData.DialogChoiceType.Fail:
                    Events.InvokeDialogEnd(DialogData.DialogChoiceType.Fail);
                    break;
            }
        }

        private void DialogEnd(DialogData.DialogChoiceType result)
        {
            gameObject.SetActive(false);
            currentDialog = -1;
        }
    }

}