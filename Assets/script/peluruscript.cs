using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peluruscript : MonoBehaviour
{
    [HideInInspector] public int damage = 10;

    private void Start()
    {
        Destroy(gameObject, 5);
    }
}
