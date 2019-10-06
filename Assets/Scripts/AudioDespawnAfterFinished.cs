//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

namespace SkeletonMistake
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioDespawnAfterFinished : MonoBehaviour
    {
        private AudioSource source = null;

        private void Awake()
        {
            source = GetComponent<AudioSource>();
        }

        private void Start()
        {
            InvokeRepeating("Check", 0.0f, 1.0f);
        }

        private void Check()
        {
            if (!source.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
}
