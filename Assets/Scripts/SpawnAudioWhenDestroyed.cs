using UnityEngine;

namespace SkeletonMistake
{
    public class SpawnAudioWhenDestroyed : MonoBehaviour
    {
        [SerializeField] private GameObject audioObject = null;

        private bool spawnAudio = true;

        private void OnDestroy()
        {
            if (audioObject != null && spawnAudio)
            {
                Instantiate(audioObject, transform.position, Quaternion.identity, null);
            }
        }

        private void OnApplicationQuit()
        {
            spawnAudio = false;
        }
    }
}
