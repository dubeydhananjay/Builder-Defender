using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    private AudioSource audioSource;
    private float volume = 0.5f;
    public float Volume { get { return volume; } }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            volume = PlayerPrefs.GetFloat(PlayerPrefConstants.MUSICVOLUME, 0.5f);
            DontDestroyOnLoad(gameObject);
        }
        else
            gameObject.SetActive(false);
    }

    public void SetVolume(bool flag)
    {
        //true: increase,  false: decrease
        volume += flag ? 0.1f : -0.1f;
        volume = Mathf.Clamp(volume, 0, 1);
        audioSource.volume = volume;
        PlayerPrefs.GetFloat(PlayerPrefConstants.MUSICVOLUME, volume);
    }
}
