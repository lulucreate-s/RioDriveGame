using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class PlayerData
{
    public string username;
    public int playerID;
    public long createdAt;
    public float money;
    public int level;
    public int totalDistance;
    public int maxSpeed;
    public int racesWon;
    public int challengesCompleted;

    // Avatar
    public int skinTone;
    public int hairstyle;
    public int hairColor;
    public int clothesTorso;
    public int clothesLegs;
    public int shoes;
    public int accessories;
}

[System.Serializable]
public class VehicleData
{
    public int vehicleID;
    public string vehicleName;
    public int modelVariant;
    public int colorPrimary;
    public int colorSecondary;
    public float topSpeed;
    public float acceleration;
    public float braking;
    public float handling;
    public int wheelType;
    public bool isOwned;
    public bool isLocked;
    public int unlockCost;
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    [SerializeField] private bool autoSave = true;
    [SerializeField] private float autoSaveInterval = 60f;

    private PlayerData currentPlayerData;
    private List<VehicleData> vehicleDatabase = new List<VehicleData>();
    private float timeSinceLastSave = 0f;

    public event Action<PlayerData> OnPlayerDataLoaded;
    public event Action<PlayerData> OnPlayerDataSaved;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadPlayerData();
        InitializeVehicleDatabase();
    }

    private void Update()
    {
        if (!autoSave) return;

        timeSinceLastSave += Time.deltaTime;
        if (timeSinceLastSave >= autoSaveInterval)
        {
            SavePlayerData();
            timeSinceLastSave = 0f;
        }
    }

    public void CreateNewPlayer(string username)
    {
        currentPlayerData = new PlayerData
        {
            username = username,
            playerID = GeneratePlayerID(),
            createdAt = DateTime.Now.Ticks,
            money = Constants.STARTING_MONEY,
            level = 1,
            totalDistance = 0,
            maxSpeed = 0,
            racesWon = 0,
            challengesCompleted = 0,
            skinTone = 0,
            hairstyle = 0,
            hairColor = 0,
            clothesTorso = 0,
            clothesLegs = 0,
            shoes = 0,
            accessories = 0
        };

        SavePlayerData();
        OnPlayerDataLoaded?.Invoke(currentPlayerData);
    }

    public void SavePlayerData()
    {
        if (currentPlayerData == null) return;

        string json = JsonUtility.ToJson(currentPlayerData);
        PlayerPrefs.SetString(Constants.PLAYER_PREFS_PREFIX + "PlayerData", json);
        PlayerPrefs.Save();

        OnPlayerDataSaved?.Invoke(currentPlayerData);
        Debug.Log($"[DataManager] Player data saved: {currentPlayerData.username}");
    }

    public void LoadPlayerData()
    {
        string key = Constants.PLAYER_PREFS_PREFIX + "PlayerData";
        
        if (!PlayerPrefs.HasKey(key))
        {
            Debug.Log("[DataManager] No player data found. New game.");
            return;
        }

        string json = PlayerPrefs.GetString(key);
        currentPlayerData = JsonUtility.FromJson<PlayerData>(json);
        OnPlayerDataLoaded?.Invoke(currentPlayerData);
        Debug.Log($"[DataManager] Player data loaded: {currentPlayerData.username}");
    }

    public PlayerData GetCurrentPlayer() => currentPlayerData;

    public bool HasPlayerData() => currentPlayerData != null;

    public void UpdatePlayerMoney(float amount)
    {
        if (currentPlayerData != null)
        {
            currentPlayerData.money += amount;
            currentPlayerData.money = Mathf.Max(0, currentPlayerData.money);
        }
    }

    public void UpdatePlayerStats(int distance, int speed, bool raceWon = false)
    {
        if (currentPlayerData == null) return;

        currentPlayerData.totalDistance += distance;
        if (speed > currentPlayerData.maxSpeed)
            currentPlayerData.maxSpeed = speed;
        if (raceWon)
            currentPlayerData.racesWon++;
    }

    private void InitializeVehicleDatabase()
    {
        vehicleDatabase.Clear();

        // Starter vehicle - Black Italian Supercar
        vehicleDatabase.Add(new VehicleData
        {
            vehicleID = 1,
            vehicleName = "Nero Veloce V12",
            modelVariant = 0,
            colorPrimary = 0, // Black
            colorSecondary = 1, // Red accents
            topSpeed = 320f,
            acceleration = 95f,
            braking = 90f,
            handling = 80f,
            wheelType = 0,
            isOwned = true,
            isLocked = false,
            unlockCost = 0
        });

        // Add more vehicles (examples)
        vehicleDatabase.Add(new VehicleData
        {
            vehicleID = 2,
            vehicleName = "Urban Cruiser",
            modelVariant = 1,
            colorPrimary = 2,
            colorSecondary = 3,
            topSpeed = 180f,
            acceleration = 70f,
            braking = 85f,
            handling = 90f,
            wheelType = 0,
            isOwned = false,
            isLocked = false,
            unlockCost = 15000
        });
    }

    public List<VehicleData> GetVehicleDatabase() => vehicleDatabase;

    public VehicleData GetVehicleByID(int vehicleID)
    {
        return vehicleDatabase.Find(v => v.vehicleID == vehicleID);
    }

    private int GeneratePlayerID()
    {
        return UnityEngine.Random.Range(100000, 999999);
    }

    public void DeletePlayerData()
    {
        PlayerPrefs.DeleteKey(Constants.PLAYER_PREFS_PREFIX + "PlayerData");
        currentPlayerData = null;
        Debug.Log("[DataManager] Player data deleted.");
    }
}
