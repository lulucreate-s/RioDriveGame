using UnityEngine;

public static class Constants
{
    // Game Settings
    public const string GAME_TITLE = "RioDrive";
    public const string GAME_VERSION = "1.0.0";
    public const float TARGET_FPS = 60f;

    // Player
    public const int MAX_USERNAME_LENGTH = 20;
    public const int MIN_USERNAME_LENGTH = 3;

    // Vehicle
    public const float MAX_VEHICLE_SPEED = 250f; // km/h
    public const float VEHICLE_ACCELERATION = 50f;
    public const float VEHICLE_BRAKING = 80f;
    public const int STARTING_MONEY = 50000;

    // World
    public const float WORLD_SIZE = 50000f; // meters
    public const float CITY_CENTER_X = 0f;
    public const float CITY_CENTER_Z = 0f;

    // NPCs & Traffic
    public const int MAX_NPCs = 100;
    public const int MAX_TRAFFIC_VEHICLES = 50;
    public const float NPC_SPAWN_DISTANCE = 500f;
    public const float NPC_DESPAWN_DISTANCE = 1000f;

    // UI
    public const float HUD_FADE_SPEED = 2f;
    public const int MINIMAP_SIZE = 256;

    // Time & Weather
    public const float DAY_CYCLE_DURATION = 3600f; // 1 hour in game
    public const float GAME_HOUR_MULTIPLIER = 60f; // 1 real second = 60 game seconds

    // Save System
    public const string PLAYER_PREFS_PREFIX = "RioDrive_";
    public const string SAVE_FILE_EXTENSION = ".json";
}
