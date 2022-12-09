using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon",menuName = "Gun")]
public class itemclass : ScriptableObject
{
    public GameObject gun;
    public int maxmagazine = 40;
    public int currentmagazine = 40;
    public int stockmagazine = 270;

}
