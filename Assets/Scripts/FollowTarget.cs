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
        [SerializeField] private bool lockX = true;
        [SerializeField] private bool lockY = false;

        private Camera cam = null;

        private void Awake()
        {
            if (cam == null)
            {
                cam = GetComponent<Camera>();
            }
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

                transform.position = newPos + offset;
            }
        }
    }
}
