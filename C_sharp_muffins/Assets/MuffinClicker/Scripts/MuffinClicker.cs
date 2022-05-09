using UnityEngine;
using TMPro;

public class MuffinClicker : MonoBehaviour
{
    
    AudioSource aud;
    

    [SerializeField]
    float minSpeedX, maxSpeedX, minSpeedY, maxSpeedY;

    [SerializeField]
    TMP_Text textRewardPrefab;

    /// <summary>
    /// Creates the text reward popups, plays the 'pop' audio clip, and handles the random transform of the text reward popups.
    /// </summary>
    public void OnMuffinClicked()
    {
        //Debug.Log("Muffin Clicked");


        int addedMuffins = GameManager.Instance.AddMuffins(true);
       
        
        // Spawn the prefab
        TMP_Text textRewardCopy = Instantiate(textRewardPrefab, transform);
        textRewardCopy.text = $"+{addedMuffins}";
        textRewardCopy.transform.localPosition = Tools.GetRandomVector2(minSpeedX, maxSpeedX, minSpeedY, maxSpeedY);

        //play audio
        aud.PlayOneShot(aud.clip);
        Debug.Log("sound played");

        GameManager.Instance.AddMuffins(true);
    }



    private void Awake()
    {
        aud = gameObject.GetComponent<AudioSource>();
    }


}