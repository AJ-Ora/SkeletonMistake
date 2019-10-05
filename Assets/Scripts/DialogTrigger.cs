using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkeletonMistake
{
    public class DialogTrigger : MonoBehaviour
    {
        public int DialogIndex { get; set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            print(collision.gameObject.tag);
            if(collision.gameObject.tag != "Player")
            {
                return;
            }

            Events.InvokeDialogStart(DialogIndex);
            gameObject.SetActive(false);
        }
    }
}