﻿//using System.Collections;
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
        [SerializeField] private float airAcceleration = 40.0f;
        [SerializeField] private float maxHorizontalVelocity = 8.0f;
        [SerializeField] private float maxDropVelocity = 8.0f;
        [SerializeField] private float jumpForce = 5.0f;
        [SerializeField] private int maxMidairJumps = 3;
        [SerializeField] private float coyoteTime = 0.3f;
        [SerializeField] private Vector2 hitTakenPushForce = Vector2.zero;
        [SerializeField] private GameObject projectile = null;
        [SerializeField] private Vector3 projectileSpawnPosition = Vector3.down;
        [SerializeField] private ParticleSystem projectileParticles = null;

        [Header("Animations")]
        [SerializeField] private Animator playerAnimator = null;
        [SerializeField] private SpriteRenderer playerSprite = null;

        [Header("SFX")]
        [SerializeField] private AudioSource source = null;
        [SerializeField] private AudioClip clipJump = null;
        [SerializeField] private AudioClip clipShoot = null;
        [SerializeField] private AudioClip clipHurt = null;

        private Rigidbody2D rigid = null;
        private BoxCollider2D col = null;
        private bool isJumping = false;
        private bool isGrounded = false;
        private int currentMidairJumps = 0;
        private float currentCoyoteTime = 0.0f;

        private bool rapidFire = false;

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

        private void Start()
        {
            Events.OnDialogEnd += DialogEnd;
        }

        private void OnDestroy()
        {
            Events.OnDialogEnd -= DialogEnd;
        }

        private void DialogEnd(DialogData.DialogChoiceType result)
        {
            var healthDelta = result == DialogData.DialogChoiceType.Success ? 1 : (result == DialogData.DialogChoiceType.Fail ? -1 : 0);
            health += healthDelta;

            if(health > 3)
            {
                health = 3;
                return;
            }

            if(healthDelta < 0)
            {
                Events.InvokePlayerTakeDamage(health, Mathf.Abs(healthDelta));
            }
            else if(healthDelta > 0)
            {
                Events.InvokePlayerHeal(health, Mathf.Abs(healthDelta));
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                if (transform.position.y >= collision.transform.position.y + 0.25f)
                {
                    rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                    currentMidairJumps = maxMidairJumps;
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
            PlaySound(clipHurt, source);
            Events.InvokePlayerTakeDamage(health, 1);

            playerAnimator.SetTrigger("Is Hurt");

            if (health <= 0)
            {
                Destroy(this.gameObject);
            }
        }

        private void PlaySound(AudioClip clip, AudioSource source)
        {
            if (clip != null && source != null)
            {
                if (source.isPlaying)
                {
                    source.Stop();
                }
                source.clip = clip;
                source.Play();
            }
        }
        
        private void Update()
        {
            /* ----- UPDATE ANIMATIONS ----- */
            if (playerAnimator != null && playerSprite != null)
            {
                if (Input.GetAxisRaw(inputHorizontal) != 0.0f)
                {
                    playerSprite.flipX = (Input.GetAxisRaw(inputHorizontal) >= 0.0f ? true : false);
                }
                
                playerAnimator.SetFloat("Move Horizontal", Mathf.Abs(rigid.velocity.x));
                playerAnimator.SetFloat("Move Vertical", rigid.velocity.y);
                playerAnimator.SetBool("Is Grounded", isGrounded);
            }

            /* ----- RAPID FIRE ----- */
            if (Input.GetKeyDown(KeyCode.F4))
            {
                rapidFire = !rapidFire;
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
                    Events.InvokePlayerLand(currentMidairJumps);
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
                    if (!rapidFire)
                    {
                        isJumping = true;
                    }

                    if (isGrounded || currentMidairJumps > 0)
                    {
                        if (!isGrounded)
                        {
                            if (currentCoyoteTime <= 0.0f)
                            {
                                if (!rapidFire)
                                {
                                    currentMidairJumps--;
                                }
                                
                                if (projectile != null)
                                {
                                    Instantiate(projectile, transform.position + projectileSpawnPosition, Quaternion.identity, null);
                                    Events.InvokePlayerShoot(currentMidairJumps);
                                    PlaySound(clipShoot, source);
                                    if (projectileParticles != null)
                                    {
                                        Instantiate(projectileParticles, transform.position + projectileSpawnPosition, Quaternion.identity, null);
                                    }
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
                        if ((source.isPlaying && source.clip == clipShoot) == false)
                        {
                            PlaySound(clipJump, source);
                        }
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
                rigid.AddForce((Vector2.right * Input.GetAxis(inputHorizontal)) * (isGrounded ? acceleration : airAcceleration) * Time.fixedDeltaTime, ForceMode2D.Impulse);
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
