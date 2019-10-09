using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SkeletonMistake
{
    public class GameEnd : MonoBehaviour
    {
        [SerializeField] private string victoryText = "You win!";
        [SerializeField] private string lossText = "Game over!";
        [SerializeField] private string additionalText = "Press R to restart";

        [SerializeField] private Text textElement;

        private void Start()
        {
            Events.OnPlayerTakeDamage += PlayerTakeDamage;
            Events.OnGameWon += GameWon;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            Events.OnPlayerTakeDamage -= PlayerTakeDamage;
            Events.OnGameWon -= GameWon;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }

        private void PlayerTakeDamage(int health, int damage)
        {
            if(health > 0)
            {
                return;
            }

            gameObject.SetActive(true);
            textElement.text = lossText + "\n" + additionalText;
        }

        private void GameWon()
        {
            gameObject.SetActive(true);
            textElement.text = victoryText + "\n" + additionalText;
        }
    }

}