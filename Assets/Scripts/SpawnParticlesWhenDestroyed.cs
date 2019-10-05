using UnityEngine;

namespace SkeletonMistake
{
    public class SpawnParticlesWhenDestroyed : MonoBehaviour
    {
        [SerializeField] private ParticleSystem effectWhenDestroyed = null;

        private void OnDestroy()
        {
            if (effectWhenDestroyed != null)
            {
                Instantiate(effectWhenDestroyed, transform.position, effectWhenDestroyed.transform.rotation, null);
            }
        }
    }
}
