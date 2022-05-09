using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinscaleTransforms : MonoBehaviour
{
    [SerializeField]
    RectTransform[] spinLights;

    [SerializeField]
    float spinSpeeds = 360;

    [SerializeField]
    float scaleOffset = 1;

    [SerializeField]
    float scaleAmplitude = 0.1f;

    [SerializeField]
    float scaleSpeed = 5;


    private void Start()
    {
        // Random rotation in start
        foreach (RectTransform spinLight in spinLights)
        {
            spinLight.Rotate(0, 0, Random.Range(-10, 10));
        }
    }

    private void Update()
    {
        // Loop through lights to rotate and scale them.
        Vector3 scale = GetScaleTransforms();

        foreach (Transform spinLight in spinLights)
        {
            spinLight.Rotate(0, 0, spinSpeeds * Time.deltaTime);
            spinLight.localScale = scale;
        }
    }

    private Vector3 GetScaleTransforms()
    {
        float time = Time.time;
        float wave = Mathf.Sin(time * scaleSpeed) * scaleAmplitude;
        wave += scaleOffset;
        Vector3 scale = Vector3.one * wave;
        return scale;
    }
}
