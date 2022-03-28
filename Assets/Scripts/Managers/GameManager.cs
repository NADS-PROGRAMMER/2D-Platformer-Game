using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public RectTransform panel;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        panel.transform.localScale = Vector2.zero;
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
