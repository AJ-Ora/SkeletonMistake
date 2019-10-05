using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SkeletonMistake
{
    public class AmmoIndicator : MonoBehaviour
    {
        [SerializeField] private Text text;

        private void Start()
        {
            Events.OnPlayerShoot += PlayerShoot;
            Events.OnPlayerLand += PlayerLand;
            text.text = 3.ToString(); // purkkaa heh
        }

        private void OnDestroy()
        {
            Events.OnPlayerShoot -= PlayerShoot;
            Events.OnPlayerLand -= PlayerLand;
        }

        private void PlayerShoot(int ammoLeft)
        {
            text.text = ammoLeft.ToString();
        }

        private void PlayerLand(int ammo)
        {
            text.text = ammo.ToString();
        }
    }
}