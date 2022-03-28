using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Item", menuName="Collectible/New Collectible")]
public class Collectibles : ScriptableObject
{
    [SerializeField] public Sprite sprite;
    [SerializeField] public string name;
}
