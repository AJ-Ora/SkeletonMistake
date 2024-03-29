﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkeletonMistake
{
    public class LevelGenerator : MonoBehaviour
    {
        [Header("Variables")]
        [SerializeField] private LevelData levelCombat = null;
        [SerializeField] private LevelData levelDating = null;
        [SerializeField] private GameObject player = null;
        [SerializeField] private int generateChunks = 3;

        [SerializeField] private DialogData[] dialogs;
        [SerializeField] private Vector2Int datingCharacterPosition = new Vector2Int(10, 8);
        [SerializeField] private GameObject dialogTriggerPrefab;

        [SerializeField] private int chunksUntilWin = 10;
        [SerializeField] private GameObject winTriggerPrefab;

        private int screensCount = 0;
        private List<GameObject> screens = new List<GameObject>();

        private void Start()
        {
            if (player == null || levelCombat == null || levelDating == null)
            {
                Debug.LogError(this + " is missing parts of data!");
                enabled = false;
            }

            Events.OnGameWon += GameWon;
        }

        private void Update()
        {
            if(player == null)
            {
                return;
            }

            int playerCurrentScreen = -Mathf.FloorToInt(player.transform.position.y / levelCombat.levels[0].height);

            for (int i = playerCurrentScreen; i < playerCurrentScreen + generateChunks; i++)
            {
                if(i <= screensCount)
                {
                    continue;
                }

                DialogData dialog;
                if(i >= chunksUntilWin)
                {
                    var trigger = Instantiate(winTriggerPrefab);
                    trigger.transform.position = Vector3.down * screensCount * 16;
                    screensCount++;
                }
                else if ((dialog = GetDialog(i)) != null)
                {
                    var chunk = levelDating.levels[0];
                    var screen = GenerateChunk(levelDating, chunk, new Vector2(chunk.width, chunk.height));

                    var trigger = Instantiate(dialogTriggerPrefab);
                    trigger.GetComponent<DialogTrigger>().Dialog = dialog;
                    trigger.transform.position = Vector3.down * (screensCount - 1) * chunk.height;
                    trigger.transform.parent = screen.transform;

                    var character = Instantiate(dialog.CharacterPrefab);
                    character.transform.position = new Vector3(screen.transform.position.x + datingCharacterPosition.x, screen.transform.position.y + datingCharacterPosition.y, screen.transform.position.z);
                    character.transform.parent = screen.transform;
                }
                else
                {
                    Texture2D selectedChunk;

                    if (screensCount == 0)
                    {
                        selectedChunk = levelCombat.levels[0];
                    }
                    else
                    {
                        selectedChunk = levelCombat.levels[Random.Range(1, levelCombat.levels.Length)];
                    }
                    GenerateChunk(levelCombat, selectedChunk, new Vector2(selectedChunk.width, selectedChunk.height));
                }
            }
        }

        private void GameWon()
        {
            enabled = false;
        }

        private GameObject GenerateChunk(LevelData data, Texture2D image, Vector2 dimensions)
        {
            GameObject currentScreen = new GameObject("Screen " + screensCount);
            currentScreen.transform.parent = transform;
            currentScreen.transform.position = Vector3.down * screensCount * dimensions.y;

            screens.Add(currentScreen);
            screensCount++;

            for (int y = 0; y < dimensions.y; y++)
            {
                for (int x = 0; x < dimensions.x; x++)
                {
                    Color tileColor = image.GetPixel(x, y);
                    GenerateTile(data, tileColor, currentScreen.transform, x, y);
                }
            }

            //Generate invisible walls
            GameObject walls = new GameObject("VerticalWalls " + screensCount);
            walls.transform.position = currentScreen.transform.position;
            walls.transform.parent = currentScreen.transform;
            BoxCollider2D leftWall = walls.AddComponent<BoxCollider2D>();
            BoxCollider2D rightWall = walls.AddComponent<BoxCollider2D>();
            leftWall.size = new Vector2(1.0f, dimensions.y);
            leftWall.offset = new Vector2(-1.0f, dimensions.y / 2 - 0.5f);
            rightWall.size = new Vector2(1.0f, dimensions.y);
            rightWall.offset = new Vector2(dimensions.x, dimensions.y / 2 - 0.5f);

            return currentScreen;
        }

        private void GenerateTile(LevelData data, Color tileColor, Transform screen, int x, int y)
        {
            foreach (TileData tile in data.tiles)
            {
                if (tileColor == tile.tileColor)
                {
                    Instantiate(tile.tileObject, new Vector3(screen.position.x + x, screen.position.y + y, screen.position.z), Quaternion.identity, screen);
                }
            }
        }

        private DialogData GetDialog(int chunkIndex)
        {
            foreach(var dialog in dialogs)
            {
                if(dialog.ChunkIndex == chunkIndex)
                {
                    return dialog;
                }
            }

            return null;
        }
    }
}
