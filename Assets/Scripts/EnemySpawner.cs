using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject leftSpawner;
    [SerializeField] private GameObject rightSpawner;
    [SerializeField] private GameObject[] enemies;


    private void Start()
    {
        StartCoroutine(Spawner());
    }

    /* Runs every 2 to 5 seconds (Random) 
     This function spawns the enemy randomly between the leftSpawner 
    and rightSpawner. */
    IEnumerator Spawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(2, 5));

            int whereToSpawn = UnityEngine.Random.Range(0, 2);
            int enemyIndex = UnityEngine.Random.Range(0, enemies.Length);

            GameObject enemy = Instantiate(enemies[enemyIndex]);
            enemy.GetComponent<Zombie>().ToFollow = GameObject.FindGameObjectWithTag("Player").transform;

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
