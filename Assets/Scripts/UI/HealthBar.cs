using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SkeletonMistake
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private GameObject healthIconsParent;

        private List<Transform> healthIcons;

        private void Start()
        {
            healthIcons = new List<Transform>();
            foreach(Transform t in healthIconsParent.transform)
            {
                healthIcons.Add(t);
            }

            Events.OnPlayerHeal += PlayerHeal;
            Events.OnPlayerTakeDamage += PlayerTakeDamage;
        }

        private void OnDestroy()
        {
            Events.OnPlayerHeal -= PlayerHeal;
            Events.OnPlayerTakeDamage -= PlayerTakeDamage;
        }

        private void PlayerHeal(int health, int healAmount)
        {
            StopAllCoroutines();
            foreach (var icon in healthIcons)
            {
                icon.gameObject.SetActive(true);
            }
            for (int i = 0; i < healthIcons.Count; i++)
            {
                healthIcons[i].gameObject.SetActive(i + 1 <= health);
            }
        }

        private void PlayerTakeDamage(int health, int damage)
        {
            StartCoroutine(Flash(health));
        }

        private IEnumerator Flash(int health)
        {
            bool visible = false;
            for(int i = 0; i < 3; i++)
            {
                if (visible)
                {
                    RefreshBar(health);
                }
                else
                {
                    foreach (var icon in healthIcons)
                    {
                        icon.gameObject.SetActive(false);
                    }
                }
                visible = !visible;
                yield return new WaitForSeconds(0.5f);
            }
            RefreshBar(health);
        }

        private void RefreshBar(int health)
        {
            for (int i = 0; i < healthIcons.Count; i++)
            {
                healthIcons[i].gameObject.SetActive(i + 1 <= health);
            }
        }
    }
}
