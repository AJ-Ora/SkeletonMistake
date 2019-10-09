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

        private void DialogStart(DialogData dialog)
        {
            if(dialog.Portrait != null)
            {
                image.sprite = dialog.Portrait;
            }
        }
    }
}