using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkeletonMistake
{
    public class DialogTrigger : MonoBehaviour
    {
        public DialogData Dialog { get; set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag != "Player" || Dialog == null)
            {
                return;
            }

            Events.InvokeDialogStart(Dialog);
            gameObject.SetActive(false);
        }
    }
}