using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkeletonMistake
{
    public class PlatformUI : MonoBehaviour
    {
        private void Start()
        {
            Events.OnDialogStart += DialogStart;
            Events.OnDialogEnd += DialogEnd;
            Events.OnPlayerTakeDamage += PlayerTakeDamage;
        }

        private void OnDestroy()
        {
            Events.OnDialogStart -= DialogStart;
            Events.OnDialogEnd -= DialogEnd;
            Events.OnPlayerTakeDamage -= PlayerTakeDamage;
        }

        private void DialogStart(int dialogIndex)
        {
            gameObject.SetActive(false);
        }

        private void DialogEnd(DialogData.DialogChoiceType result)
        {
            gameObject.SetActive(true);
        }

        private void PlayerTakeDamage(int health, int damage)
        {
            if(health <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}