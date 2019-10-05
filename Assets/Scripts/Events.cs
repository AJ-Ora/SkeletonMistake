using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkeletonMistake
{
    public static class Events
    {
        public delegate void DialogStart(int dialogIndex);
        public static event DialogStart OnDialogStart = delegate { };
        public static void InvokeDialogStart(int dialogIndex) => OnDialogStart(dialogIndex);

        public delegate void DialogEnd(DialogData.DialogChoiceType result);
        public static event DialogEnd OnDialogEnd = delegate { };
        public static void InvokeDialogEnd(DialogData.DialogChoiceType result) => OnDialogEnd(result);

        public delegate void DialogSelected(int choiceIndex);
        public static event DialogSelected OnDialogSelected = delegate { };
        public static void InvokeDialogSelected(int choiceIndex) => OnDialogSelected(choiceIndex);

        public delegate void PlayerShoot(int ammoLeft);
        public static event PlayerShoot OnPlayerShoot = delegate { };
        public static void InvokePlayerShoot(int ammoLeft) => OnPlayerShoot(ammoLeft);

        public delegate void PlayerLand(int ammo);
        public static event PlayerLand OnPlayerLand = delegate { };
        public static void InvokePlayerLand(int ammo) => OnPlayerLand(ammo);

        public delegate void PlayerHeal(int health, int healAmount);
        public static event PlayerHeal OnPlayerHeal = delegate { };
        public static void InvokePlayerHeal(int health, int healAmount) => OnPlayerHeal(health, healAmount);

        public delegate void PlayerTakeDamage(int health, int damage);
        public static event PlayerTakeDamage OnPlayerTakeDamage = delegate { };
        public static void InvokePlayerTakeDamage(int health, int damage) => OnPlayerTakeDamage(health, damage);

        public delegate void GameWon();
        public static event GameWon OnGameWon = delegate { };
        public static void InvokeGameWon() => OnGameWon();
    }
}