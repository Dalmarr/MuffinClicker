using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CupcakeClicker : MonoBehaviour
{

    private float timer;
    private bool wasCupcakeClicked = false;

    [SerializeField]
    float minSpeedX, maxSpeedX, minSpeedY, maxSpeedY;

    [SerializeField]
    TMP_Text textRewardPrefab;

    [SerializeField]
    Image cupcakeImage;

    Color startColor;

    /// <summary>
    /// Creates the text reward popups, plays the 'pop' audio clip, and handles the random transform of the text reward popups.
    /// </summary>
    public void OnCupcakeClicked()
    {
        if (!wasCupcakeClicked)
        {
            //Debug.Log("Muffin Clicked");


            int addedMuffins = GameManager.Instance.AddMuffins(false);


            // Spawn the prefab
            SpawnTextRewardPrefab(addedMuffins);
            wasCupcakeClicked = true;
            

        }
    }

    private void Awake()
    {
        startColor = cupcakeImage.color;
    }

    private void Update()
    {
        if (wasCupcakeClicked)
        {
            timer += Time.deltaTime;
            cupcakeImage.color = Color.Lerp(startColor, Color.clear, timer); 
        }

        if(timer > 1)
        {
            Destroy(gameObject);
        }
    }

    private void SpawnTextRewardPrefab(int addedMuffins)
    {
        TMP_Text textRewardCopy = Instantiate(textRewardPrefab, transform.parent);
        textRewardCopy.text = $"+{addedMuffins}";
        Vector2 localRandomPos = Tools.GetRandomVector2(minSpeedX, maxSpeedX, minSpeedX, maxSpeedX);
        Vector3 worldRandomPos = transform.TransformPoint(localRandomPos); 
        textRewardCopy.transform.localPosition = worldRandomPos;
    }
}
