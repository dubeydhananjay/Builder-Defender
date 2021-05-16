using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class OptionMenuUI : MonoBehaviour
{
    private TextMeshProUGUI textSoundVolume;
    private TextMeshProUGUI textMusicVolume;
    private void Awake()
    {
        textSoundVolume = transform.Find("textSoundVolume").GetComponent<TextMeshProUGUI>();
        textMusicVolume = transform.Find("textMusicVolume").GetComponent<TextMeshProUGUI>();
        transform.Find("btnSoundDecrease").GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.Instance.SetVolume(false);
            SetVolumetext(textSoundVolume, SoundManager.Instance.Volume);
        });
        transform.Find("btnSoundIncrease").GetComponent<Button>().onClick.AddListener(() =>
          {
              SoundManager.Instance.SetVolume(true);
              SetVolumetext(textSoundVolume, SoundManager.Instance.Volume);
          });
        transform.Find("btnMusicDecrease").GetComponent<Button>().onClick.AddListener(() =>
        {
            MusicManager.Instance.SetVolume(false);
            SetVolumetext(textMusicVolume, MusicManager.Instance.Volume);
        });
        transform.Find("btnMusicIncrease").GetComponent<Button>().onClick.AddListener(() =>
        {
            MusicManager.Instance.SetVolume(true);
            SetVolumetext(textMusicVolume, MusicManager.Instance.Volume);
        });

        transform.Find("btnMainMenu").GetComponent<Button>().onClick.AddListener(() =>
        GameSceneManager.LoadScene(GameSceneManager.Scenes.MainMenuScene));
    }

    private void Start()
    {
        if (SoundManager.Instance)
            SetVolumetext(textSoundVolume, SoundManager.Instance.Volume);
        if (MusicManager.Instance)
            SetVolumetext(textMusicVolume, MusicManager.Instance.Volume);

        Visibilty();
    }

    private void SetVolumetext(TextMeshProUGUI textVolume, float val)
    {
        textVolume.text = Mathf.RoundToInt((val * 10)).ToString();
    }

    public void Visibilty()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        Time.timeScale = gameObject.activeSelf ? 0 : 1;
    }
}
