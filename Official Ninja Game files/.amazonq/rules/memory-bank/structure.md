# Project Structure

## Root Directory
```
Official Ninja Game files/
├── Assets/                    # All game assets
├── Library/                   # Unity auto-generated cache (do not edit)
├── Logs/                      # Unity editor logs
├── Packages/                  # Unity Package Manager config
├── ProjectSettings/           # Unity project settings
├── Temp/                      # Temporary build files
├── UserSettings/              # Per-user editor preferences
├── *.csproj                   # C# project files (auto-generated)
├── Official Ninja Game files.sln  # Visual Studio solution
└── *.keystore                 # Android signing keystores (actualkey, ImpossibleNinja, keys, UpdatedKey)
```

## Assets Directory (Core Game Content)
```
Assets/
├── Scripts/                   # All game logic C# scripts
│   ├── EnemyStates/          # Enemy AI state machine scripts
│   │   ├── IEnemyState.cs    # State interface
│   │   ├── Idlestate.cs      # Idle behavior
│   │   ├── PatrolState.cs    # Patrol behavior
│   │   ├── MeleeState.cs     # Close combat behavior
│   │   └── RangedState.cs    # Ranged attack behavior
│   ├── Character.cs          # Abstract base class for all characters
│   ├── Player.cs             # Player controller and mechanics
│   ├── Enemy.cs              # Enemy base logic with state machine
│   ├── GameManager1.cs       # Core game state management (switches, coins, level progression)
│   ├── Menu.cs               # In-game menu/pause state machine
│   ├── MainMenu.cs           # Main menu screen
│   ├── LevelSelector.cs      # Level selection UI with unlock system
│   ├── SaveManager.cs        # JSON-based save/load via PlayerPrefs
│   ├── SaveState.cs          # Serializable save data class
│   ├── AudioManager.cs       # Sound/music management with AudioMixer
│   ├── CameraFollow.cs       # Camera tracking with bounds clamping
│   ├── Parallexing.cs        # Parallax background scrolling
│   ├── SceneFader.cs         # Scene transition fade effects
│   ├── TextBoxManager.cs     # Dialogue display with typewriter effect
│   ├── LevelGenerator.cs     # Procedural level from color-mapped images
│   ├── AdManager.cs          # Ad monetization (placeholder for new SDK)
│   ├── PlayerStats.cs        # XP/level progression system
│   ├── Door.cs               # Door lock/unlock/open state management
│   ├── Switch.cs             # In-level switch triggers
│   ├── Knife.cs              # Projectile behavior
│   ├── EnemySight.cs         # Enemy detection trigger zone
│   ├── EnemySpawner.cs       # Timed enemy spawning
│   ├── PlatformMovement.cs   # Moving platform with player parenting
│   ├── CollisionTrigger.cs   # One-way platform collision handling
│   ├── BossWall.cs           # Wall that destroys when enemies die
│   ├── SwordCollider.cs      # Melee hit detection
│   ├── IgnoreCollision.cs    # Selective collision ignoring
│   ├── ActivateTextAtLine.cs # Trigger-based dialogue activation
│   ├── TextImporter.cs       # Text file line parser
│   ├── ColorToPrefab.cs      # Color-to-prefab mapping data class
│   ├── thronsScript.cs       # Thorn damage trigger
│   ├── SafeAreaCanvasScaler.cs # UI scaling + safe area for all screen sizes
│   └── CameraAspectAdjuster.cs # Camera ortho size adjustment for aspect ratios
├── CrossPlatformInput/        # Custom input abstraction (namespace-free, Unity 6 compatible)
│   ├── Scripts/
│   │   ├── CrossPlatformInputManager.cs  # Static input API
│   │   ├── VirtualInput.cs              # Abstract input base
│   │   ├── Joystick.cs                  # Touch joystick UI component
│   │   ├── ButtonHandler.cs             # Touch button UI component
│   │   ├── AxisTouchButton.cs           # Axis-based touch button
│   │   ├── TouchPad.cs                  # Touch pad input
│   │   ├── MobileControlRig.cs         # Mobile/desktop control switching
│   │   ├── TiltInput.cs                # Accelerometer input
│   │   ├── InputAxisScrollbar.cs       # Scrollbar axis input
│   │   └── PlatformSpecific/
│   │       ├── MobileInput.cs          # Mobile virtual input implementation
│   │       └── StandaloneInput.cs      # Desktop Input.GetAxis wrapper
│   ├── Prefabs/              # Mobile control prefabs
│   └── Sprites/              # Button/joystick sprites
├── Animations/               # Animation clips
├── AnimationBehaviours/      # StateMachineBehaviour scripts
├── Controllers/              # Animator controllers
├── Prefabs/                  # Reusable game object prefabs
├── Sprites/                  # 2D sprite artwork
├── Scenes/                   # Additional scene files
├── Music/                    # Audio files
├── Fonts/                    # Font assets
├── TextFiles/                # Dialogue text data files
├── Coins/                    # Coin/collectible assets
├── Enemy/                    # Enemy-specific assets
├── HealthBar/                # Health bar UI assets
├── tilesets/                 # Tilemap tile assets
├── DialogueArt/              # Dialogue UI artwork
├── Plugins/                  # Native plugins (Android/iOS)
├── Editor/                   # Editor-only scripts
├── TextMesh Pro/             # TextMeshPro assets (Examples & Extras removed for Unity 6)
└── *.unity                   # Scene files (levels, menus, shop)
```

## Scene Files (Build Order)
- `MainMenu.unity` - Title/main menu (index 0)
- `LevelSelect.unity` - Level selection screen (index 1)
- `2D Platformer.unity` - Level 1 (index 2)
- `Level02.unity` - Level 2 (index 3)
- `Level03.unity` - Level 3 (index 4)
- `Level04.unity` - Level 4 (index 5)
- `Level05.unity` - Level 5 (index 6)
- `Shop.unity` - In-game shop

## Architecture Patterns
- **Inheritance Hierarchy**: `Character` (abstract) → `Player` / `Enemy`
- **State Pattern**: Enemy AI uses `IEnemyState` interface with concrete states (Idle, Patrol, Melee, Ranged)
- **Singleton Managers**: GameManager1, AudioManager, SaveManager, Menu, Door, LevelSelector, SceneFader
- **Component-Based**: Standard Unity MonoBehaviour composition
- **Custom Stat Type**: `stat` class with `CurrentVal`, `MaxVal`, `Initialize()` for health
- **Coroutine-Based**: Damage, death, immortality, knockback, and scene fading use Unity coroutines
- **Event System**: `DeadEventHandler` delegate for player death notifications to enemies

## Key Relationships
- `Player` and `Enemy` both inherit from `Character`
- `Enemy` subscribes to `Player.Dead` event to clear targets
- `GameManager1` manages coins, switches, and level completion
- `Menu` controls UI state and delegates to `GameManager1` for level loading
- `Door` checks `GameManager1.noOfSwitches` for open/close state
- `Switch` triggers are polled by `GameManager1.GetNoOfSwitches()`
- `EnemySight` sets `Enemy.Target` on player detection
