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

        private void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
            Events.OnDialogStart += DialogStart;
        }

        private void OnDestroy()
        {
            button?.onClick.RemoveListener(OnClick);
            Events.OnDialogStart -= DialogStart;
        }

        private void OnClick()
        {
            Events.InvokeDialogSelected(choiceIndex);
        }

        private void DialogStart(int dialogIndex)
        {
            var entry = DialogManager.Instance.GetEntry(dialogIndex);
            if (entry == null)
            {
                return;
            }

            var choice = DialogManager.Instance.GetChoice(entry, choiceIndex);
            button.enabled = choice != null;
        }
    }
}
