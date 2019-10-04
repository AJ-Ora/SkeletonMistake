//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

namespace SkeletonMistake
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField] private string inputHorizontal = "Horizontal";
        [SerializeField] private string inputJump = "Jump";

        [Header("Variables")]
        [SerializeField] private float acceleration = 80.0f;
        [SerializeField] private float maxHorizontalVelocity = 8.0f;
        [SerializeField] private float jumpForce = 5.0f;

        private Rigidbody2D rigid = null;
        private bool isJumping = false;
        private bool isGrounded = false;

        private void Awake()
        {
            if (rigid == null)
            {
                rigid = GetComponent<Rigidbody2D>();
            }
        }
        
        private void FixedUpdate()
        {
            if (Input.GetButton(inputJump))
            {
                if (!isJumping)
                {
                    isJumping = true;
                    rigid.velocity = new Vector2(rigid.velocity.x, 0.0f);
                    rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }
            }
            else
            {
                isJumping = false;
            }
            
            rigid.AddForce((Vector2.right * Input.GetAxis(inputHorizontal)) * acceleration * Time.fixedDeltaTime, ForceMode2D.Impulse);

            if (Mathf.Abs(rigid.velocity.x) > maxHorizontalVelocity)
            {
                rigid.velocity = new Vector2(Mathf.Sign(rigid.velocity.x) * maxHorizontalVelocity, rigid.velocity.y);
            }
        }
    }
}
