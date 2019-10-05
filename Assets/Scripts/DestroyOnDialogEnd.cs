using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkeletonMistake
{
    public class DestroyOnDialogEnd : MonoBehaviour
    {
        private void Start()
        {
            Events.OnDialogEnd += DialogEnd;
        }

        private void OnDestroy()
        {
            Events.OnDialogEnd -= DialogEnd;
        }

        private void DialogEnd(DialogData.DialogChoiceType result)
        {
            Destroy(gameObject);
        }
    }
}