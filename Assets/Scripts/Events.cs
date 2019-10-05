using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkeletonMistake
{
    public static class Events
    {
        public delegate void DialogStart(int dialogIndex);
        public delegate void DialogEnd(DialogData.DialogChoiceType result);
        public delegate void DialogSelected(int choiceIndex);

        public static event DialogStart OnDialogStart = delegate { };
        public static event DialogEnd OnDialogEnd = delegate { };
        public static event DialogSelected OnDialogSelected = delegate { };

        public static void InvokeDialogStart(int dialogIndex) => OnDialogStart(dialogIndex);
        public static void InvokeDialogEnd(DialogData.DialogChoiceType result) => OnDialogEnd(result);
        public static void InvokeDialogSelected(int choiceIndex) => OnDialogSelected(choiceIndex);
    }
}