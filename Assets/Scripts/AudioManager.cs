using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _musicSource;

    private void Awake() => instance = this;

    public void EnableAudio(bool isEnable) => _audioSource.enabled = isEnable;
    public void EnableMusic(bool isEnalbe) => _musicSource.enabled = isEnalbe;

    public void PlayAudio(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}