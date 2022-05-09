using System;
using TMPro;
using UnityEngine;

public class HeaderUI : MonoBehaviour
{
    [SerializeField]
    TMP_Text totalMuffinText;

    [SerializeField]
    TMP_Text _muffinsPerSecond;


    private void Start()
    {
        Instance_OnTotalMuffinsChanged();
        UpdatemuffinsPerSecond(0);
        GameManager.Instance.OnTotalMuffinsChanged += Instance_OnTotalMuffinsChanged;
        GameManager.Instance.OnMuffinsPerSecondChanged += UpdatemuffinsPerSecond;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnTotalMuffinsChanged -= Instance_OnTotalMuffinsChanged;
        GameManager.Instance.OnMuffinsPerSecondChanged -= UpdatemuffinsPerSecond;
    }

    private void Instance_OnTotalMuffinsChanged()
    {
        TMP_Text myText = totalMuffinText;
        int totalMuffins = GameManager.Instance.TotalMuffins;
        myText.text =
            totalMuffins == 1 ?
            "1 Muffin" :
            $"{totalMuffins} Muffins";
    }

    private void UpdatemuffinsPerSecond(int muffinsPerSecond) =>
        _muffinsPerSecond.text = $"{muffinsPerSecond} muffins/sec";
    
}
