using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TMPro.TextMeshProUGUI killsText;
    public TMPro.TextMeshProUGUI killsTextInPanel;
    public TMPro.TextMeshProUGUI highestKillsText;

    int kills;
    int highestKills;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        killsText.text = "Kills: " + kills.ToString();
        killsTextInPanel.text = "Kills: " + kills.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
       highestKills = PlayerPrefs.GetInt("h-kills", 0);
       highestKillsText.text = "Highest Kills: " + highestKills.ToString();
    }
    
    public void UpdateKill()
    {
        kills++;

        if (kills > highestKills)
        {
            highestKills = kills;
            PlayerPrefs.SetInt("h-kills", kills);
            highestKillsText.text = "NEW HIGHEST KILLS: " + highestKills.ToString();
        }
        else
        {
            highestKillsText.text = "HIGHEST KILLS: " + highestKills.ToString();
        }
    }
}
