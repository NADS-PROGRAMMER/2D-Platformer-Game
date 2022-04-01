using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public RectTransform panel;
    public TMPro.TextMeshProUGUI textScore;
    public TMPro.TextMeshProUGUI textHighestScore;

    // Data to Save
    //[HideInInspector]
    public int score;
    public int highestScore = 0;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        panel.transform.localScale = Vector2.zero;
        this.highestScore = PlayerPrefs.GetInt("Highest Score", 0);
    }


    private void FixedUpdate()
    {
        textScore.text = "Your Score: " +  this.score.ToString();

        if (this.score > this.highestScore)
        {
            this.highestScore = this.score;
            PlayerPrefs.SetInt("Highest Score", this.highestScore);
        }
        textHighestScore.text = "Highest Score: " + this.highestScore.ToString();
    }


    public void GameOver()
    {
        Opponent[] opponents = FindObjectsOfType<Opponent>();

        FindObjectOfType<EnemySpawner>().gameObject.SetActive(false);

        foreach (Opponent opponent in opponents)
        {
            Destroy(opponent.gameObject);
        }
        panel.LeanScale(Vector2.one, .5f);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
