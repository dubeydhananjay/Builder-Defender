using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    private void Awake()
    {
        transform.Find("btnPlay").GetComponent<Button>().onClick.AddListener(() =>
        GameSceneManager.LoadScene(GameSceneManager.Scenes.GameScene));

        transform.Find("btnQuit").GetComponent<Button>().onClick.AddListener(() =>
      Application.Quit());

    }
}
