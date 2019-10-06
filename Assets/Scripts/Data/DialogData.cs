using System.Collections.Generic;
using UnityEngine;

namespace SkeletonMistake
{
    [CreateAssetMenu]
    public class DialogData : ScriptableObject
    {
        public enum DialogChoiceType
        {
            NextDialog,
            Success,
            Fail
        }

        [System.Serializable]
        public class DialogChoice
        {
            public string Text;
            public DialogChoiceType ChoiceType = DialogChoiceType.NextDialog;
            public int NextDialogIndex;
        }

        [System.Serializable]
        public class DialogEntry
        {
            public string Subtitle;
            public List<DialogChoice> Choices;
        }

        public List<DialogEntry> Entries;
        public Sprite Portrait;
        public GameObject CharacterPrefab;
        public int ChunkIndex;
    }
}