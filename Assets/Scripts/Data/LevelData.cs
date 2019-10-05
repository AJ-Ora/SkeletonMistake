using UnityEngine;

namespace SkeletonMistake
{
    [CreateAssetMenu(fileName = "Level Data Asset", menuName = "Level Generation/Level Data", order = 1)]
    public class LevelData : ScriptableObject
    {
        public Texture2D[] levels = null;
        public TileData[] tiles = null;
    }
}
