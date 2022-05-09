using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour
{

    [SerializeField]
    float minSpeed, maxSpeed;

    private static float moveSpeed;
    private void Awake()
    {
        moveSpeed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 movement = new Vector3(0, Time.deltaTime * moveSpeed, 0);
        transform.Translate(movement, Space.Self);

    }
}
