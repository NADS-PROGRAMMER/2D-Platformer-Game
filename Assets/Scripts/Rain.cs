using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    [SerializeField]
    public Transform camera;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPos = transform.position;
        currentPos.x = camera.transform.position.x;
        transform.position = currentPos;
    }
}
