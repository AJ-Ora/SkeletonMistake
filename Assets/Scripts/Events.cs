using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkeletonMistake
{
    public static class Events
    {
        public delegate void DialogStart(DialogData dialog);
        public static event DialogStart OnDialogStart = delegate { };
        public static void InvokeDialogStart(DialogData dialog) => OnDialogStart(dialog);

        public delegate void DialogEnd(DialogData.DialogChoiceType result);
        public static event DialogEnd OnDialogEnd = delegate { };
        public static void InvokeDialogEnd(DialogData.DialogChoiceType result) => OnDialogEnd(result);

        public delegate void DialogEntryChange(DialogData dialog, int entryIndex);
        public static event DialogEntryChange OnDialogEntryChange = delegate { };
        public static void InvokeDialogEntryChange(DialogData dialog, int entryIndex) => OnDialogEntryChange(dialog, entryIndex);

        public delegate void DialogSelected(DialogData dialog, int choiceIndex);
        public static event DialogSelected OnDialogSelected = delegate { };
        public static void InvokeDialogSelected(DialogData dialog, int choiceIndex) => OnDialogSelected(dialog, choiceIndex);

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