using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Random = UnityEngine.Random; <---  this is to set a definition for what Random class we want to use

public class GameManager : MonoBehaviour
{
    int totalMuffins;
    const int CritMultiplier = 10;
    const string saveSlot = "save1";

    private int muffinsPerClick = 1;

    [SerializeField]
    float critChance = 0.01f;

    private float _muffinsPerSecondTimer;
    private int _muffinsPerSecond;
    private UpgradeButton[] _upgradeButtons;



    public static GameManager Instance;

    public event Action OnTotalMuffinsChanged;
    public event Action<int> OnMuffinsPerSecondChanged;

    public int TotalMuffins
    {
        get 
        { 
            return totalMuffins; 
        }    
        private set 
        { 
            totalMuffins = value;
            // The question mark is used to check for null values before running the Invoke() function. 
            // This is in case there is nothing subscribed to the event to avoid throwing an error.
            OnTotalMuffinsChanged?.Invoke(); 
            
        }
    }
    /// <summary>
    /// Adds muffins to the total muffin count.
    /// </summary>
    /// <returns>the added muffins</returns>
    public int AddMuffins(bool isMuffin)
    {

        // Crit chance
        int addedMuffins = 0;

        if(!isMuffin)
        {
            addedMuffins = muffinsPerClick * 100;
        }
        else if (UnityEngine.Random.value <= critChance)
        {
            addedMuffins = muffinsPerClick * CritMultiplier;
        }
        else
        {
            addedMuffins = muffinsPerClick;
        }

        TotalMuffins += addedMuffins;
        return addedMuffins;
    }

    public bool TryPurchaseUpgrade(int currentCost, int level, UpgradeType upgradeType)
    {
        if (TotalMuffins >= currentCost)
        {
            TotalMuffins -= currentCost;
            level++;
            ApplyUpgrade(level, upgradeType);

            if (upgradeType == UpgradeType.Muffin)
            {
            }
            else
            {
                muffinsPerClick = level * 2;
            }
            return true;
        }

        return false;
    }

    private void ApplyUpgrade(int level, UpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case UpgradeType.Muffin:
                muffinsPerClick = 1 + level * 5;
                break;
            case UpgradeType.Milkshake:
                muffinsPerClick = level * 2;
                OnMuffinsPerSecondChanged?.Invoke(_muffinsPerSecond);
                break;
            case UpgradeType.FancyMuffin:
                muffinsPerClick = 1 + level * 20;
                break;
        }
    }

    /// <summary>
    /// Serializes the runtime data, and saves the serialized data
    /// </summary>
    private void Save()
    {
        SaveData saveData;
        saveData.totalMuffins = TotalMuffins;
        UpgradeButton[] buttons =  FindObjectsOfType<UpgradeButton>();
        saveData.upgrades = new List<SaveableUpgrade>(buttons.Length);

        foreach(UpgradeButton button in buttons)
        {
            SaveableUpgrade upgrade;
            upgrade.level = button.Level;
            upgrade.upgradeType = button.upgradeType;
            saveData.upgrades.Add(upgrade);
        }

        // Serialize our runtime data
        string serializedData = JsonUtility.ToJson(saveData);

        // Save the serialized data
        PlayerPrefs.SetString(saveSlot, serializedData);
    }

    private void Start()
    {
        Load();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void Load()
    {
        if(!PlayerPrefs.HasKey(saveSlot))
        {
            return;
        }
        // Load the serialized data
        string loadedData = PlayerPrefs.GetString(saveSlot);

        // Deserialize the serialized data
        SaveData deserializedData = JsonUtility.FromJson<SaveData>(loadedData);

        // Apply the data
        TotalMuffins = deserializedData.totalMuffins;

        UpgradeButton[] buttons = FindObjectsOfType<UpgradeButton>();

        foreach (UpgradeButton button in buttons)
        {
            foreach (SaveableUpgrade upgrade in deserializedData.upgrades)
            {
                if(button.upgradeType == upgrade.upgradeType)
                {
                    button.Level = upgrade.level;
                    ApplyUpgrade(button.Level, button.upgradeType);
                    break;
                }
            }



        }
    }

    /// <summary>
    /// Holds all the data that is intended for saving
    /// </summary>
    [System.Serializable]
    private struct SaveData
    {
        public int totalMuffins;
        public List<SaveableUpgrade> upgrades;
    }

    [System.Serializable]
    private struct SaveableUpgrade
    {
        public UpgradeType upgradeType;
        public int level;
    }

    private void Awake()
    {
        Instance = this;
        _upgradeButtons = FindObjectsOfType<UpgradeButton>();
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }

        // Reset the game
        if (Input.GetKeyDown(KeyCode.R))
        {
            TotalMuffins = 0;
            UpgradeButton[] buttons = FindObjectsOfType<UpgradeButton>();

            foreach (UpgradeButton button in buttons)
            {
                button.Level = 0;
                ApplyUpgrade(0, button.upgradeType);


            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        _muffinsPerSecondTimer += Time.deltaTime;
        if(_muffinsPerSecondTimer >= 1)
        {
            _muffinsPerSecondTimer--;
            TotalMuffins += _muffinsPerSecond;
        }
    }


    // this method is for Unity games with multiple scenes. 

    //private void Awake()
    //{
    //    if(Instance == null)
    //    {
    //        Instance = this;
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

}
