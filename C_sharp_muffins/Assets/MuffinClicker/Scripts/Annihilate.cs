using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Annihilate : MonoBehaviour
{
    float timer;

    [SerializeField]
    private float destroyTime = 1;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
    }

