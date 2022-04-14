using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    public RectTransform bossText;
    public float time;
    public bool isNotOver = false;

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
        time -= 1 * Time.deltaTime;

        if (time <= 0 && !isNotOver)
        {
            bossText.LeanScale(Vector2.one, .5f);
            GameObject boss = Instantiate(Boss);
            boss.transform.position = Spawner.transform.GetChild(1).position;
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
