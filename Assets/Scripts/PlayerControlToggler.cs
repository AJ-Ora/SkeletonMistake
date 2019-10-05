using System.Collections;
using UnityEngine;

namespace SkeletonMistake
{
    public class PlayerControlToggler : MonoBehaviour
    {
        private PlayerController controller;

        void Start()
        {
            controller = GetComponent<PlayerController>();
            Events.OnDialogStart += DialogStart;
            Events.OnDialogEnd += DialogEnd;
        }

        private void OnDestroy()
        {
            Events.OnDialogStart -= DialogStart;
            Events.OnDialogEnd -= DialogEnd;
        }

        private void DialogStart(int dialogIndex)
        {
            controller.enabled = false;
        }

        private void DialogEnd(DialogData.DialogChoiceType result)
        {
            StartCoroutine(EnableControls());
        }

        private IEnumerator EnableControls()
        {
            yield return new WaitForSeconds(1);
            controller.enabled = true;
        }
    }

}