using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkeletonMistake
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Projectile : MonoBehaviour
    {
        [Header("Variables")]
        [SerializeField] private float velocity = 10.0f;
        [SerializeField] private Vector2 direction = Vector2.down;

        private Rigidbody2D rigid = null;
        private BoxCollider2D col = null;
        private bool hit = false;

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
            rigid.MovePosition(rigid.position + direction * velocity * Time.fixedDeltaTime);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!hit)
            {
                if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Breakable")
                {
                    Destroy(collision.gameObject);
                }
            }
            
            hit = true;
            Destroy(this.gameObject);
        }
    }
}
