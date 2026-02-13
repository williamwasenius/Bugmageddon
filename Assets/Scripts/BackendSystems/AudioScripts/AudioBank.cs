using AudioSystem;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Audio Bank")]
public class AudioBank : ScriptableObject
{
    public string categoryName;
    public AudioData[] sounds;
}
