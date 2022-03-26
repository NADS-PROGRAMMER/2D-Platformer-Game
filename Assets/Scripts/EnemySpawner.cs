using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject leftSpawner;
    [SerializeField] private GameObject rightSpawner;
    [SerializeField] private GameObject[] enemies;

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 5));

            int whereToSpawn = Random.Range(0, 2);
            int enemyIndex = Random.Range(0, enemies.Length);

            GameObject enemy = Instantiate(enemies[enemyIndex]);
            enemy.GetComponent<Zombie>().ToFollow =  GameObject.FindGameObjectWithTag("Player").transform;

            if (whereToSpawn == 0)
            {
                enemy.transform.position = leftSpawner.transform.position;
            }
            else
            {
                enemy.transform.position = rightSpawner.transform.position;
            }
        }
    }
}
