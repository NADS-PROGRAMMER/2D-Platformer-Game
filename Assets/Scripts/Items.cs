using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] SpriteRenderer renderer;
    [SerializeField] private Collectibles collectible;
    public int value;

    private void Start()
    {
        renderer.sprite = collectible.sprite;
    }
}
