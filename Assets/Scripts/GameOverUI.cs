using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameOverUI : MonoBehaviour
{

    public static GameOverUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        transform.Find("btnRetry").GetComponent<Button>().onClick.AddListener(() =>
         GameSceneManager.LoadScene(GameSceneManager.Scenes.GameScene));
        transform.Find("btnMainMenu").GetComponent<Button>().onClick.AddListener(() =>
         GameSceneManager.LoadScene(GameSceneManager.Scenes.MainMenuScene));
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        TextMeshProUGUI textWavesSurvived = transform.Find("textWavesSurvived").GetComponent<TextMeshProUGUI>();
        textWavesSurvived.text = "You survived " + EnemyWaveManager.Instance.WaveNumber + " waves!";
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }


}
