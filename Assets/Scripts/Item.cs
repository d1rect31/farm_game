using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public string id;
    public string description;
    public Sprite sprite;
    public GameObject prefab; // Префаб, который будет создан при взаимодействии
    public bool isSeed; // Флаг, указывающий, является ли предмет семенами
}
