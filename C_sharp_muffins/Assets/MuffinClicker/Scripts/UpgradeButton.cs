using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _levelText, _costText;

    [SerializeField]
    private int[] _costPerLevel;

    private int _level;

    public UpgradeType upgradeType;

    public int Level
    {
        get => _level;
        set
        {
            _level = value;
            _levelText.text = _level.ToString();
            _costText.text = CurrentCost.ToString();
        }
    }

    private int CurrentCost
    {
        get
        {
            return 5 + _level * 5;
            //    int lengthOfCosts = _costPerLevel.Length;
            //    if(lengthOfCosts == 0) {
            //        return 0;
            //    }

            //    if (_level >= lengthOfCosts)
            //    {
            //        return _costPerLevel[lengthOfCosts -1];
            //    }
            //    return _costPerLevel[_level];
            //}
        }
    }
    private void Awake() =>
        GetComponent<Button>().onClick.AddListener(OnUpgradeClicked);

    private void OnUpgradeClicked()
    {
        Debug.Log("Upgrade clicked");
        if (GameManager.Instance.TryPurchaseUpgrade(CurrentCost, Level, upgradeType))
        {
            Level++;

        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnTotalMuffinsChanged -= Instance_OnTotalMuffinsChanged;
    }

    private void Start()
    {

        GameManager.Instance.OnTotalMuffinsChanged += Instance_OnTotalMuffinsChanged;
        Level = 0;
    }

    private void Instance_OnTotalMuffinsChanged()
    {
        bool canAfford = GameManager.Instance.TotalMuffins >= CurrentCost;

        _costText.color =
            canAfford ?
            Color.green :
            Color.red;
    }
}


