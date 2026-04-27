using System;
using UnityEngine;
using UnityEngine.Audio;

namespace AudioSystem
{
    public enum audioDataType
        {
            Music,
            Ambience,
            UI,
            Weapon,
            Explosion,
            Enviromental,
            Misc
        }

    [CreateAssetMenu(menuName = "Audio/Audio Data")]
    public class AudioData : ScriptableObject
    {
        public string id;
        public audioDataType dataType;
        public AudioClip clip;
        public AudioMixerGroup mixerGroup;

        [Range(0f, 1f)] public float volume = 1f;
        [Range(0.1f, 3f)] public float pitch = 1f;

        public bool playOnAwake = true;
        public bool loop = false;
        public bool positionalSound = false;
        public float minDistance = 10f;
        public float maxDistance = 100f;
        public AudioRolloffMode rolloffMode = AudioRolloffMode.Linear;
    }


}
