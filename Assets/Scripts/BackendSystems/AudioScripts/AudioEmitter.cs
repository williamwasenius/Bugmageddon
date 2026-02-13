using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace AudioSystem
{
    public class AudioEmitter : MonoBehaviour
    {

        AudioSource audioSource;
        Coroutine playingCoroutine;

        private void Awake()
        {
            audioSource = gameObject.GetOrAddComponent<AudioSource>();
        }

        public void Play()
        {
            if (playingCoroutine != null)
            {
                StopCoroutine(playingCoroutine);
            }

            audioSource.Play();
            playingCoroutine = StartCoroutine(waitForSoundToEnd());

        }

        IEnumerator waitForSoundToEnd()
        {
            yield return new WaitWhile(() => audioSource.isPlaying);
            AudioManager.Instance.ReturnToPool(this);
        }

        public void Stop()
        {
            if (playingCoroutine != null)
            {
                StopCoroutine(playingCoroutine);
                playingCoroutine = null;
            }

            audioSource.Stop();
            AudioManager.Instance.ReturnToPool(this);
        }

        public void Initialize(AudioData data)
        {
            audioSource.clip = data.clip;
            audioSource.outputAudioMixerGroup = data.mixerGroup;
            audioSource.loop = data.loop;
            audioSource.playOnAwake = data.playOnAwake;
        }

        public void WithRandomPitch(float min = -0.05f, float max = 0.05f)
        {
            audioSource.pitch += Random.Range(min, max);
        }

    }
}
