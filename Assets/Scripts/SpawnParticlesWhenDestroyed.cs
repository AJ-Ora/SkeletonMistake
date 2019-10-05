using UnityEngine;

namespace SkeletonMistake
{
    public class SpawnParticlesWhenDestroyed : MonoBehaviour
    {
        [SerializeField] private ParticleSystem effectWhenDestroyed = null;

        private bool spawnParticles = true;

        private void OnDestroy()
        {
            if (effectWhenDestroyed != null && spawnParticles)
            {
                Instantiate(effectWhenDestroyed, transform.position, effectWhenDestroyed.transform.rotation, null);
            }
        }

        private void OnApplicationQuit()
        {
            spawnParticles = false;
        }
    }
}
