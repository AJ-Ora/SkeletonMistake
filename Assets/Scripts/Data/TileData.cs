using UnityEngine;

namespace SkeletonMistake
{
    [CreateAssetMenu(fileName = "Tile Data Asset", menuName = "Level Generation/Tile Data", order = 2)]
    public class TileData : ScriptableObject
    {
        public string tileName = "";
        public Color tileColor = Color.white;
        public GameObject tileObject = null;
    }
}
