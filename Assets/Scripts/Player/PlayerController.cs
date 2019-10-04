//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

namespace SkeletonMistake
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField] private string inputHorizontal = "Horizontal";
        [SerializeField] private string inputJump = "Jump";

        [Header("Variables")]
        [SerializeField] private float acceleration = 80.0f;
        [SerializeField] private float maxHorizontalVelocity = 8.0f;
        [SerializeField] private float jumpForce = 5.0f;
        [SerializeField] private int maxJumps = 3;

        private Rigidbody2D rigid = null;
        private BoxCollider2D col = null;
        private bool isJumping = false;
        private bool isGrounded = false;
        private int currentJumps = 0;

        private void Awake()
        {
            if (rigid == null)
            {
                rigid = GetComponent<Rigidbody2D>();
            }

            if (col == null)
            {
                col = GetComponent<BoxCollider2D>();
            }
        }

        private void FixedUpdate()
        {
            /* ----- CHECK IF GROUNDED ----- */

            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position + Vector3.left * col.size.x / 2 + Vector3.down * col.size.y / 2, Vector2.down, 0.05f);
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position + Vector3.right * col.size.x / 2 + Vector3.down * col.size.y / 2, Vector2.down, 0.05f);
            
            if (hitLeft.collider != null || hitRight.collider != null)
            {
                if (hitLeft.collider != col && hitRight.collider != col)
                {
                    isGrounded = true;
                    currentJumps = maxJumps;
                }
                else
                {
                    isGrounded = false;
                }
            }
            else
            {
                isGrounded = false;
            }

            Debug.DrawRay(transform.position + Vector3.left * col.size.x / 2 + Vector3.down * col.size.y / 2, Vector2.down * 0.05f, isGrounded ? Color.green : Color.yellow);
            Debug.DrawRay(transform.position + Vector3.right * col.size.x / 2 + Vector3.down * col.size.y / 2, Vector2.down * 0.05f, isGrounded ? Color.green : Color.yellow);

            /* ----- DO SOME PHYSICS BASED ON INPUTS ----- */

            if (Input.GetButton(inputJump))
            {
                if (!isJumping)
                {
                    isJumping = true;

                    if (currentJumps > 0)
                    {
                        rigid.velocity = new Vector2(rigid.velocity.x, 0.0f);
                        rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                        currentJumps--;
                    }
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
