# RioDriveGame - Project Structure

## 📁 Dossier Organisation

```
Assets/
├── Scripts/
│   ├── Core/
│   │   ├── GameManager.cs
│   │   ├── DataManager.cs
│   │   └── AudioManager.cs
│   ├── Player/
│   │   ├── PlayerCreation.cs
│   │   ├── PlayerProfile.cs
│   │   └── AvatarCustomization.cs
│   ├── Vehicle/
│   │   ├── VehicleController.cs
│   │   ├── VehiclePhysics.cs
│   │   ├── VehicleGarage.cs
│   │   └── VehicleDatabase.cs
│   ├── Input/
│   │   ├── MobileInputManager.cs
│   │   ├── VirtualJoystick.cs
│   │   └── VirtualPedals.cs
│   ├── UI/
│   │   ├── MenuManager.cs
│   │   ├── HUD.cs
│   │   ├── MiniMap.cs
│   │   └── UIManager.cs
│   ├── World/
│   │   ├── CityGenerator.cs
│   │   ├── RoadNetwork.cs
│   │   ├── TerrainManager.cs
│   │   └── TimeWeatherManager.cs
│   ├── NPC/
│   │   ├── NPCManager.cs
│   │   ├── NPCBehaviour.cs
│   │   └── TrafficAI.cs
│   ├── Challenges/
│   │   ├── ChallengeManager.cs
│   │   ├── RaceChallenge.cs
│   │   ├── TimeTrialChallenge.cs
│   │   └── SpeedChallenge.cs
│   └── Utils/
│       ├── Constants.cs
│       ├── Helpers.cs
│       └── SaveSystem.cs
├── Prefabs/
│   ├── Vehicles/
│   ├── NPCs/
│   ├── UI/
│   └── Objects/
├── Scenes/
│   ├── 00_SplashScreen.unity
│   ├── 01_PlayerCreation.unity
│   ├── 02_MainMenu.unity
│   ├── 03_GameWorld.unity
│   ├── 04_Garage.unity
│   └── 05_Challenges.unity
├── Models/
│   ├── Vehicles/
│   ├── Environment/
│   ├── NPCs/
│   └── Props/
├── Textures/
├── Materials/
├── Audio/
│   ├── Engine/
│   ├── SFX/
│   ├── Music/
│   └── Ambience/
└── Resources/
    ├── Data/
    └── Config/
```

## 🎯 Phases de Développement

### Phase 1: Fondations
- [x] Structure du projet
- [ ] Système de données (PlayerPrefs + JSON)
- [ ] Game Manager
- [ ] Écran de création du joueur

### Phase 2: Contrôles & Véhicules
- [ ] Système d'input mobile
- [ ] Contrôles tactiles (joystick + pédales)
- [ ] Physique des véhicules
- [ ] Garage et sélection de voiture

### Phase 3: Monde Ouvert
- [ ] Génération de la ville Rio
- [ ] Réseau routier
- [ ] Terrain et topographie
- [ ] Optimisation mobile

### Phase 4: Contenu & Gameplay
- [ ] PNJ et trafic IA
- [ ] Système de défis
- [ ] Cycle jour/nuit
- [ ] Météo

### Phase 5: Polish & Optimisation
- [ ] Son et musique
- [ ] Effets visuels
- [ ] Performance mobile
- [ ] Menu et UI

---

## 💾 Convention de Nommage

- **Classes**: `PascalCase` (ex: `VehicleController`)
- **Méthodes**: `PascalCase` (ex: `StartGame()`)
- **Variables**: `camelCase` (ex: `playerName`)
- **Constantes**: `UPPER_SNAKE_CASE` (ex: `MAX_SPEED`)
- **Événements**: `On + Action` (ex: `OnPlayerCreated`)
