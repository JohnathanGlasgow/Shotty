using UnityEngine;
using UnityEngine.Audio;

public class MuteAudio : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string groupName; // Name of the audio group you want to mute

    public void MuteGroup(bool mute)
    {
        float volume = mute ? -80f : 0f; // -80 dB mutes the audio
        audioMixer.SetFloat(groupName + "Volume", volume);
    }
}
