using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SkeletonMistake
{
    public class DialogCharacterPortrait : MonoBehaviour
    {
        private Image image;

        private void Start()
        {
            image = GetComponent<Image>();
            Events.OnDialogStart += DialogStart;
        }

        private void OnDestroy()
        {
            Events.OnDialogStart -= DialogStart;
        }

        private void DialogStart(int dialogIndex)
        {
            var entry = DialogManager.Instance.GetEntry(dialogIndex);

            if(entry?.Portrait != null)
            {
                image.sprite = entry.Portrait;
            }
        }
    }
}