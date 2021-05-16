using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum Sound
    {
        BuildingPlaced,
        BuildingDamaged,
        BuildingDestroyed,
        EnemyHit,
        EnemyDie,
        EnemyWaveStarting,
        GameOver
    }

    public static SoundManager Instance { get; private set; }
    private Dictionary<Sound, AudioClip> audioClipDictionary;
    private AudioSource audioSource;
    private float volume = 0.5f;
    public float Volume { get { return volume; } }

    private void Awake()
    {
        Instance = this;
        volume = PlayerPrefs.GetFloat(PlayerPrefConstants.SOUNDVOLUME, 0.5f);
        audioSource = GetComponent<AudioSource>();
        audioClipDictionary = new Dictionary<Sound, AudioClip>();
        foreach (Sound sound in System.Enum.GetValues(typeof(Sound)))
            audioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());

    }

    public void PlayAudio(Sound sound)
    {
        AudioClip clip = audioClipDictionary[sound];
        audioSource.PlayOneShot(clip, volume);
    }

    public void SetVolume(bool flag)
    {
        //true: increase,  false: decrease
        volume += flag ? 0.1f : -0.1f;
        volume = Mathf.Clamp(volume, 0, 1);
        PlayerPrefs.GetFloat(PlayerPrefConstants.SOUNDVOLUME, volume);
    }


}
