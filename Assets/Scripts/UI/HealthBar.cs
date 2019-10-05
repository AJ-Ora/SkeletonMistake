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

            Events.OnPlayerTakeDamage += PlayerTakeDamage;
        }

        private void OnDestroy()
        {
            Events.OnPlayerTakeDamage -= PlayerTakeDamage;
        }

        private void PlayerTakeDamage(int health, int damage)
        {
            for(int i = 0; i < healthIcons.Count; i++)
            {
                healthIcons[i].gameObject.SetActive(i + 1 <= health);
            }
        }
    }
}
