using AudioSystem;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

namespace AudioSystem
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [SerializeField] private AudioBank[] banks;

        private Dictionary<string, AudioData> lookup;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            lookup = new Dictionary<string, AudioData>();

            foreach (var bank in banks)
            {
                foreach (var sound in bank.sounds)
                {
                    if (!lookup.ContainsKey(sound.id))
                        lookup.Add(sound.id, sound);
                    else
                        Debug.LogWarning($"Duplicate audio id: {sound.id}");
                }
            }
        }

        public AudioSource Play(string audioID, Vector3 position)
        {
            if (!lookup.TryGetValue(audioID, out var audioData))
            {
                Debug.LogWarning($"Sound not found: {audioID}");
                return null;
            }

            GameObject gameObj = new GameObject($"Audio_{audioData.id}");
            gameObj.transform.position = position;

            AudioSource source = gameObj.AddComponent<AudioSource>();
            source.clip = audioData.clip;
            source.volume = audioData.volume;
            source.pitch = audioData.pitch;
            source.loop = audioData.loop;
            source.outputAudioMixerGroup = audioData.mixerGroup;
            if (audioData.positionalSound)
            {
                source.spatialBlend = 1f;
            }
            else
            {
                source.spatialBlend = 0f;
            }

                source.Play();

            if (!audioData.loop)
            {
                Destroy(gameObj, audioData.clip.length / audioData.pitch);
            }

            return source;
        }



        public IEnumerator PlayMusic(string introId, string loopId)
        {
            AudioData introData = null;
            AudioData loopData = null;

            if (!string.IsNullOrEmpty(introId))
            {
                if (!lookup.TryGetValue(introId, out introData) || introData.clip == null)
                {
                    Debug.LogWarning($"Intro music not found or invalid: {introId}");
                    yield break;
                }
            }

            if (!string.IsNullOrEmpty(loopId))
            {
                if (!lookup.TryGetValue(loopId, out loopData) || loopData.clip == null)
                {
                    Debug.LogWarning($"Loop music not found or invalid: {loopId}");
                    yield break;
                }
            }

            if (introData == null && loopData == null)
                yield break;

            if (introData != null)
            {
                AudioSource introAudio = Play(introData.id, Vector3.zero);
                yield return new WaitForSecondsRealtime(introData.clip.length);
                Destroy(introAudio.gameObject);
            }

            if (loopData != null)
            {
                Play(loopData.id, Vector3.zero);
            }
        }

        IObjectPool<AudioEmitter> audioEmitterPool;
        readonly List<AudioEmitter> activeAudioEmitters = new();
        public readonly Dictionary<AudioData, int> counts = new();

        [SerializeField] AudioEmitter audioEmitterPrefab;
        [SerializeField] bool collectionCheck = true;
        [SerializeField] int defaultCapacity = 10;
        [SerializeField] int maxPoolSize = 100;
        [SerializeField] int maxAudioInstances = 30;

        private void Start()
        {
            InitializePool();
        }

        public bool CanPlaySound(AudioData data)
        {
            if (counts.TryGetValue(data, out var count))
            {
                if (count >= maxAudioInstances)
                {
                    return false;
                }
            }
            return true;
        }

        public AudioEmitter Get()
        {
            return audioEmitterPool.Get();
        }

        public void ReturnToPool(AudioEmitter audioEmitter)
        {
            audioEmitterPool.Release(audioEmitter);
        }

        void OnDestroyPoolObject(AudioEmitter audioEmitter)
        {
            Destroy(audioEmitter.gameObject);
        }

        void OnReturnToPool(AudioEmitter audioEmitter)
        {
            audioEmitter.gameObject.SetActive(false);
            activeAudioEmitters.Remove(audioEmitter);
        }

        void OnTakeFromPool(AudioEmitter audioEmitter)
        {
            audioEmitter.gameObject.SetActive(true);
            activeAudioEmitters.Add(audioEmitter);
        }

        AudioEmitter CreateAudioEmitter()
        {
            var audioEmitter = Instantiate(audioEmitterPrefab);
            audioEmitter.gameObject.SetActive(false);
            return audioEmitter;
        }

        void InitializePool()
        {
            audioEmitterPool = new ObjectPool<AudioEmitter>(
                CreateAudioEmitter,
                OnTakeFromPool,
                OnReturnToPool,
                OnDestroyPoolObject,
                collectionCheck,
                defaultCapacity,
                maxPoolSize
                );
        }

    }
}
