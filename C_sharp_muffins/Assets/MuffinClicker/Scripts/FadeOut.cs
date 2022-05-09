using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    float timer;
    Color startColor;

    [SerializeField]
    TMP_Text tmpText;

    [SerializeField]
    private float fadeTime = 0.5f;

    // Update is called once per frame
    public void Update()
    {
        timer += Time.deltaTime;
        tmpText.color = Color.Lerp(startColor, Color.clear, timer/fadeTime);
    }

    private void Awake()
    {
        startColor = tmpText.color;
    }
}
