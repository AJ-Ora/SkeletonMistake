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
            Events.OnDialogStart += DialogStart;
        }

        private void OnDestroy()
        {
            Events.OnDialogStart -= DialogStart;
        }

        private void DialogStart(int dialogIndex)
        {
            var entry = DialogManager.Instance.GetEntry(dialogIndex);
            if (entry == null)
            {
                return;
            }

            var choice = DialogManager.Instance.GetChoice(entry, choiceIndex);
            text.text = choice?.Text ?? "";
        }
    }
}
