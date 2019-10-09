using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkeletonMistake
{
    [RequireComponent(typeof(Camera))]
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] private Transform followTarget = null;
        [SerializeField][Range(0.0f, 1.0f)] private float followSpeed = 0.7f;
        [SerializeField] private Vector3 offset = -Vector3.forward;
        [SerializeField] private Vector3 offsetWhenDating = Vector3.zero;
        [SerializeField] private bool lockX = true;
        [SerializeField] private bool lockY = false;

        private Camera cam = null;
        private bool isDating = false;

        private void Awake()
        {
            if (cam == null)
            {
                cam = GetComponent<Camera>();
            }
        }

        private void Start()
        {
            Events.OnDialogStart += DialogStart;
            Events.OnDialogEnd += DialogEnd;
        }

        private void OnDestroy()
        {
            Events.OnDialogStart -= DialogStart;
            Events.OnDialogEnd -= DialogEnd;
        }

        private void DialogStart(DialogData dialog)
        {
            isDating = true;
        }

        private void DialogEnd(DialogData.DialogChoiceType result)
        {
            isDating = false;
        }

        void LateUpdate()
        {
            if (followTarget != null)
            {
                Vector3 newPos = Vector3.zero;

                if (!lockX)
                {
                    newPos.x = Mathf.Lerp(transform.position.x, followTarget.position.x, followSpeed);
                }

                if (!lockY)
                {
                    newPos.y = Mathf.Lerp(transform.position.y, followTarget.position.y, followSpeed);
                }

                transform.position = newPos + (isDating ? offsetWhenDating : offset);
            }
        }
    }
}
