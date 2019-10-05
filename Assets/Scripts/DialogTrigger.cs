using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkeletonMistake
{
    public class DialogTrigger : MonoBehaviour
    {
        [SerializeField] private int dialogIndex;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag != "Player")
            {
                return;
            }

            Events.InvokeDialogStart(dialogIndex);
            gameObject.SetActive(false);
        }
    }
}