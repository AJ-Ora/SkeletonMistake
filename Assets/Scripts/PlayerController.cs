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
        [SerializeField] private int health = 3;
        [SerializeField] private float acceleration = 80.0f;
        [SerializeField] private float maxHorizontalVelocity = 8.0f;
        [SerializeField] private float maxDropVelocity = 8.0f;
        [SerializeField] private float jumpForce = 5.0f;
        [SerializeField] private int maxMidairJumps = 3;
        [SerializeField] private float coyoteTime = 0.3f;
        [SerializeField] private Vector2 hitTakenPushForce = Vector2.zero;
        [SerializeField] private GameObject projectile = null;
        [SerializeField] private Vector3 projectileSpawnPosition = Vector3.down;

        private Rigidbody2D rigid = null;
        private BoxCollider2D col = null;
        private bool isJumping = false;
        private bool isGrounded = false;
        private int currentMidairJumps = 0;
        private float currentCoyoteTime = 0.0f;

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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                if (transform.position.y >= collision.transform.position.y + 0.25f)
                {
                    rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                    Destroy(collision.gameObject);
                }
                else
                {
                    OnHurt();
                    rigid.AddForce((transform.position.x >= collision.transform.position.x ? Vector2.right : Vector2.left) * hitTakenPushForce.x + Vector2.up * hitTakenPushForce.y, ForceMode2D.Impulse);
                }
            }
        }

        private void OnHurt()
        {
            health--;
            
            if (health <= 0)
            {
                Destroy(this.gameObject);
            }
        }

        private void FixedUpdate()
        {
            /* ----- CHECK IF GROUNDED ----- */

            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position + Vector3.left * (col.size.x / 2.0f + col.edgeRadius) + Vector3.down * (col.size.y / 2.0f + col.edgeRadius), Vector2.down, 0.05f);
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position + Vector3.right * (col.size.x / 2.0f + col.edgeRadius) + Vector3.down * (col.size.y / 2.0f + col.edgeRadius), Vector2.down, 0.05f);
            
            if (hitLeft.collider != null || hitRight.collider != null)
            {
                if (hitLeft.collider != col && hitRight.collider != col)
                {
                    isGrounded = true;
                    currentMidairJumps = maxMidairJumps;
                    currentCoyoteTime = coyoteTime;
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

            currentCoyoteTime -= isGrounded ? 0.0f : Time.fixedDeltaTime;
            
            Debug.DrawRay(transform.position + Vector3.left * (col.size.x / 2.0f + col.edgeRadius) + Vector3.down * (col.size.y / 2.0f + col.edgeRadius), Vector2.down * 0.05f, isGrounded ? Color.green : Color.yellow);
            Debug.DrawRay(transform.position + Vector3.right * (col.size.x / 2.0f + col.edgeRadius) + Vector3.down * (col.size.y / 2.0f + col.edgeRadius), Vector2.down * 0.05f, isGrounded ? Color.green : Color.yellow);

            /* ----- DO SOME PHYSICS BASED ON INPUTS ----- */

            if (Input.GetButton(inputJump))
            {
                if (!isJumping)
                {
                    isJumping = true;

                    if (isGrounded || currentMidairJumps > 0)
                    {
                        if (!isGrounded)
                        {
                            if (currentCoyoteTime <= 0.0f)
                            {
                                currentMidairJumps--;
                                if (projectile != null)
                                {
                                    Instantiate(projectile, transform.position + projectileSpawnPosition, Quaternion.identity, null);
                                }
                                else
                                {
                                    Debug.LogWarning(this + " tried to shoot a projectile, but none have been assigned!");
                                }
                            }
                        }
                        currentCoyoteTime = 0.0f;
                        rigid.velocity = new Vector2(rigid.velocity.x, 0.0f);
                        rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                    }
                }
            }
            else
            {
                isJumping = false;
            }
            
            RaycastHit2D hitWallUp = Physics2D.Raycast(transform.position + (Input.GetAxisRaw(inputHorizontal) >= 0 ? Vector3.right : Vector3.left) * (col.size.x / 2.0f + col.edgeRadius) + Vector3.up * (col.size.y / 2.0f + col.edgeRadius), (Input.GetAxisRaw(inputHorizontal) >= 0 ? Vector3.right : Vector3.left), 0.05f);
            RaycastHit2D hitWallDown = Physics2D.Raycast(transform.position + (Input.GetAxisRaw(inputHorizontal) >= 0 ? Vector3.right : Vector3.left) * (col.size.x / 2.0f + col.edgeRadius) + Vector3.down * (col.size.y / 2.0f + col.edgeRadius), (Input.GetAxisRaw(inputHorizontal) >= 0 ? Vector3.right : Vector3.left), 0.05f);

            if ((hitWallUp.collider == null || hitWallUp.collider == col) && (hitWallDown.collider == null || hitWallDown.collider == col))
            {
                rigid.AddForce((Vector2.right * Input.GetAxis(inputHorizontal)) * acceleration * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }
            
            /* ----- LIMIT VELOCITY ----- */

            if (Mathf.Abs(rigid.velocity.x) > maxHorizontalVelocity)
            {
                rigid.velocity = new Vector2(Mathf.Sign(rigid.velocity.x) * maxHorizontalVelocity, rigid.velocity.y);
            }

            if (rigid.velocity.y < -maxDropVelocity)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, Mathf.Sign(rigid.velocity.y) * maxDropVelocity);
            }
        }
    }
}
