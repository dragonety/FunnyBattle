using System.Collections;
using UnityEngine;

namespace Reconstitution {
    public class Effect : MonoBehaviour {

        [HideInInspector]
        public string path;

        private float duration;
        private ParticleSystem[] particles;

        private void Awake() {
            particles = GetComponentsInChildren<ParticleSystem>();
            duration = GetMaxDuration();
        }

        public void Play(bool isOneTime) {
            foreach (ParticleSystem particle in particles) {
                if (!particle.isPlaying) {
                    particle.Play();
                }
            }
            if (isOneTime) {
                StartCoroutine(OnRecycle());
            }
        }

        public void Stop() {
            foreach (ParticleSystem particle in particles) {
                if (particle.isPlaying) {
                    particle.Stop();
                }
            }
        }

        private float GetMaxDuration() {
            float max = 0;
            foreach (ParticleSystem particle in particles) {
                max = Mathf.Max(max, particle.main.duration);
            }
            return max;
        }

        IEnumerator OnRecycle() {
            yield return new WaitForSeconds(duration);
            Stop();
            CachedManager.effectCachedPool.Recycle(this);
        }

    }
}