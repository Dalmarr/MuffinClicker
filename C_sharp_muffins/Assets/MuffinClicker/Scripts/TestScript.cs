using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public static int globalNum = 2;
    public int myNumbers;
    private void Start()
    {
        globalNum = myNumbers;
    }
}
