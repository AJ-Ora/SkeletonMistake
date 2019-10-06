using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkeletonMistake
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicController : MonoBehaviour
    {
        [SerializeField] private AudioClip clipAction = null;
        [SerializeField] private AudioClip clipDating = null;

        private AudioSource source = null;

        private void Awake()
        {
            source = GetComponent<AudioSource>();
        }

        private void Start()
        {
            Events.OnDialogStart += DialogStarted;
            Events.OnDialogEnd += DialogEnded;
        }

        private void OnDestroy()
        {
            Events.OnDialogStart -= DialogStarted;
            Events.OnDialogEnd -= DialogEnded;
        }

        private void DialogStarted(int dialogIndex)
        {
            source.Stop();
            source.clip = clipDating;
            source.Play();
        }

        private void DialogEnded(DialogData.DialogChoiceType result)
        {
            source.Stop();
            source.clip = clipAction;
            source.Play();
        }
    }
}
