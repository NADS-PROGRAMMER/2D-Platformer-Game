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
    public TMPro.TextMeshProUGUI textScore;
    public TMPro.TextMeshProUGUI kills;
    public TMPro.TextMeshProUGUI textHighestKills;
    private bool isResume = false;
    public GameObject enemySpawner;

    // Data to Save
    [HideInInspector]
    public int noOfKills;
    [HideInInspector]
    public int highestScore = 0;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        if (panel != null)
        {
            panel.transform.localScale = Vector2.zero;
            this.highestScore = PlayerPrefs.GetInt("Highest Kills", 0);
        }

        if (pausePanel != null)
        {
            pausePanel.transform.localScale = Vector3.zero;
        }
    }


    private void Update()
    {
        // Check if the Game Manager is used in actual game play.
        if (textScore == null)
        {
            return;
        }

        // Updates both text UI for current kills
        textScore.text = "KILLS: " + this.noOfKills.ToString();
        kills.text = "KILLS: " + this.noOfKills.ToString();

        if (this.noOfKills > this.highestScore)
            PlayerPrefs.SetInt("Highest Kills", this.noOfKills);

        textHighestKills.text = "Highest Kills: " + PlayerPrefs.GetInt("Highest Kills");

        /* */
        if (Input.GetKeyDown(KeyCode.Escape) && enemySpawner.activeSelf)
        {
            if (!isResume)
            {
                pausePanel.LeanScale(Vector2.one, .5f);
                isResume = true;
                Invoke("Pause", .5f);
            }
            else
            {
                Time.timeScale = 1;
                isResume = false;
                pausePanel.LeanScale(Vector2.zero, .5f);
                
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


    public void Test()
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