using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkeletonMistake
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class EnemyRoaming : MonoBehaviour
    {
        [Header("Variables")]
        public bool isMovingRight = false;

        [SerializeField] private float acceleration = 80.0f;
        [SerializeField] private float maxHorizontalVelocity = 8.0f;

        private Rigidbody2D rigid = null;
        private BoxCollider2D col = null;

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
            if (collision.gameObject.tag == "Player")
            {
                rigid.velocity = new Vector2(0.0f, rigid.velocity.y);
                isMovingRight = !isMovingRight;
            }
        }

        private void FixedUpdate()
        {
            /* ----- CHECK IF ENEMY SHOULD CHANGE DIRECTION ----- */

            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position + Vector3.left * (col.size.x / 2.0f + col.edgeRadius) + Vector3.down * (col.size.y / 1.99f + col.edgeRadius), Vector2.down, 0.99f);
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position + Vector3.right * (col.size.x / 2.0f + col.edgeRadius) + Vector3.down * (col.size.y / 1.99f + col.edgeRadius), Vector2.down, 0.99f);

            Debug.DrawRay(transform.position + Vector3.left * (col.size.x / 2.0f + col.edgeRadius) + Vector3.down * (col.size.y / 1.99f + col.edgeRadius), Vector2.down * 0.99f, Color.green);
            Debug.DrawRay(transform.position + Vector3.right * (col.size.x / 2.0f + col.edgeRadius) + Vector3.down * (col.size.y / 1.99f + col.edgeRadius), Vector2.down * 0.995f, Color.green);

            RaycastHit2D hitWallUp = Physics2D.Raycast(transform.position + (isMovingRight ? Vector3.right : Vector3.left) * (col.size.x / 1.99f + col.edgeRadius) + Vector3.up * (col.size.y / 2.0f + col.edgeRadius), (isMovingRight ? Vector3.right : Vector3.left), 0.05f);
            RaycastHit2D hitWallDown = Physics2D.Raycast(transform.position + (isMovingRight ? Vector3.right : Vector3.left) * (col.size.x / 1.99f + col.edgeRadius) + Vector3.down * (col.size.y / 2.0f + col.edgeRadius), (isMovingRight ? Vector3.right : Vector3.left), 0.05f);
            
            if (((isMovingRight ? hitRight.collider : hitLeft.collider) == null || (isMovingRight ? hitRight.collider : hitLeft.collider) == col)
                || (hitWallUp.collider != null && hitWallUp.collider != col)
                || (hitWallDown.collider != null && hitWallDown.collider != col))
            {
                isMovingRight = !isMovingRight;
            }

            /* ----- APPLY & LIMIT VELOCITY ----- */

            rigid.AddForce(Vector2.right * (isMovingRight ? 1.0f : -1.0f) * acceleration * Time.fixedDeltaTime, ForceMode2D.Impulse);

            if (Mathf.Abs(rigid.velocity.x) > maxHorizontalVelocity)
            {
                rigid.velocity = new Vector2(Mathf.Sign(rigid.velocity.x) * maxHorizontalVelocity, rigid.velocity.y);
            }
        }
    }
}
