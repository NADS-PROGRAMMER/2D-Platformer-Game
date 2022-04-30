using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public RectTransform text;
    public RectTransform panel;
    public RectTransform skillPanel;
    public RectTransform pausePanel;
    public GameObject enemySpawner;
    private bool isResume = false;

    private void Awake()
    {
        if (panel != null)
        {
            panel.transform.localScale = Vector2.zero;
        }

        if (pausePanel != null)
        {
            pausePanel.transform.localScale = Vector3.zero;
        }
    }


    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Update()
    {
        /* */
        if (Input.GetKeyUp(KeyCode.Escape) && enemySpawner.activeSelf)
        {
            if (!isResume)
            {
                pausePanel.LeanScale(Vector2.one, .1f);
                isResume = true;
                Invoke("Pause", .1f);
            }
            else
            {
                Time.timeScale = 1;
                isResume = false;
                pausePanel.LeanScale(Vector2.zero, .1f);
            }
        }
    }


    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    
    public void GameOver()
    {
        this.Test();
    }


    public void Win()
    {
        text.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "WINNER";

        this.Test();
    }


    public void Pause()
    {
        Time.timeScale = 0;
    }


    private void Test()
    {
        Boss[] opponents = FindObjectsOfType<Boss>(); // Get the gameObjects holding that holding the opponent script.
        TimeManager.instance.gameObject.SetActive(false);
        skillPanel.gameObject.SetActive(false); // Disable the skill panel.
        enemySpawner.SetActive(false); // Disable the spawner.

        // Destroy all of the enemy game objects.
        foreach (Boss opponent in opponents)
        {
            Destroy(opponent.gameObject);
        }

        // Show the panel.
        panel.LeanScale(Vector2.one, .5f);
    }
}