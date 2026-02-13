using AudioSystem;
using UnityEngine;

public class AudioBuilder : MonoBehaviour
{
    readonly AudioManager audioManager;
    AudioData audioData;
    Vector3 position = Vector3.zero;
    bool randomPitch;

    public AudioBuilder(AudioManager audioManager)
    {
        this.audioManager = audioManager;
    }

    public AudioBuilder WithAudioData(AudioData audioData)
    {
        this.audioData = audioData;
        return this;
    }

    public AudioBuilder WithPosition(Vector3 position)
    {
        this.position = position;
        return this;
    }

    public AudioBuilder WithRandomPitch()
    {
        this.randomPitch = true;
        return this;
    }

    public void Play()
    {
        if (!audioManager.CanPlaySound(audioData)) return;

        AudioEmitter emitter = audioManager.Get();
        emitter.Initialize(audioData);
        emitter.transform.position = position;
        emitter.transform.parent = AudioManager.Instance.transform;

        if (randomPitch)
        {
            emitter.WithRandomPitch();
        }

        if (audioManager.counts.TryGetValue(audioData, out var count))
        {
            audioManager.counts[audioData] = count + 1;
        }
        else
        {
            audioManager.counts[audioData] = 1;
        }

    }
}
