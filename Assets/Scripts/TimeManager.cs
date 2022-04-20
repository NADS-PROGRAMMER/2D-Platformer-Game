using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    public RectTransform bossText;
    public bool isNotOver = false;
    public float time;

    [Header("Need Objects")]
    public GameObject Spawner;
    public GameObject Boss;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        bossText.transform.localScale = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        /* Bawasan yung time by 1 proportional sa passed frame. */
        time -= 1 * Time.deltaTime;

        /* To avoid na mag run ito kahit less than or equal to 0 na ang time,
         we set a boolean variable "isNotOver" (By Default is False) to check it. */
        if (time <= 0 && !isNotOver)
        {
            bossText.LeanScale(Vector2.one, .5f);
            GameObject boss = Instantiate(Boss);
            boss.transform.position = new Vector2(Spawner.transform.GetChild(1).position.x, boss.transform.position.y);
            Spawner.gameObject.SetActive(false);
            isNotOver = true;
            StartCoroutine("CloseText");
        }
    }

    IEnumerator CloseText()
    {
        yield return new WaitForSeconds(2);

        bossText.LeanScale(Vector2.zero, .5f);
    }
}
