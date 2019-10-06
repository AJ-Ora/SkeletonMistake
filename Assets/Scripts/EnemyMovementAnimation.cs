using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkeletonMistake
{
    [RequireComponent(typeof(Animator))]
    public class EnemyMovementAnimation : MonoBehaviour
    {
        [SerializeField] private EnemyRoaming roamingComponent;
        [SerializeField] private string idleAnimation = "idle";
        [SerializeField] private string walkAnimation = "walk";

        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            var localScale = transform.localScale;
            localScale.x = roamingComponent.isMovingRight ? -1 : 1;
            transform.localScale = localScale;

            var xVelocity = roamingComponent.GetComponent<Rigidbody2D>().velocity.x;
            var animationToPlay = Mathf.Abs(xVelocity) > 0 ? walkAnimation : idleAnimation;
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName(animationToPlay))
            {
                animator.Play(animationToPlay);
            }
        }
    }
}