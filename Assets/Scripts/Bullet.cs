using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float bulletSpeed;
    public bool isFlipped;

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPos = transform.position;

        if (isFlipped)
        {
            currentPos.x -= bulletSpeed * Time.deltaTime;
        } 
        else
        {
            currentPos.x += bulletSpeed * Time.deltaTime;
        }
        transform.position = currentPos;
    }
}
