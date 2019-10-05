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
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            Events.InvokeDialogSelected(choiceIndex);
        }
    }
}
