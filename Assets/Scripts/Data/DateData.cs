using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkeletonMistake
{
    [CreateAssetMenu]
    public class DateData : ScriptableObject
    {
        [System.Serializable]
        public class Date
        {
            public string Name;
            public int ChunkIndex;
            public int DialogIndex;
            public GameObject CharacterPrefab;
        }

        public Date[] Dates;
    }
}