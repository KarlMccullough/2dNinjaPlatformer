# Development Guidelines

## Code Quality Standards

### Naming Conventions
- **Classes**: PascalCase (e.g., `GameManager1`, `LevelSelector`, `EnemySight`)
- **Public fields**: camelCase (e.g., `collectedCoins`, `nextLevel`, `canMove`)
- **Private fields**: camelCase with no prefix (e.g., `immortal`, `dropItem`)
- **Serialized private fields**: camelCase with `[SerializeField]` attribute (e.g., `[SerializeField] private float jumpForce`)
- **Properties**: PascalCase (e.g., `Instance`, `IsDead`, `OnGround`, `CollectedCoins`)
- **Methods**: PascalCase (e.g., `HandleMovement`, `ChangeDirection`, `ThrowKnife`)
- **Enums**: PascalCase for both type and values (e.g., `MenuStates.Playing`)
- **Parameters**: camelCase (e.g., `horizontal`, `knockbackPwr`, `newState`)

### Class Structure Pattern
Classes follow this ordering convention:
```csharp
public class ClassName : MonoBehaviour
{
    // 1. Private static instance field
    // 2. Serialized fields ([SerializeField])
    // 3. Public fields
    // 4. Private fields
    // 5. Static Instance property (singleton)
    // 6. Other properties
    // 7. Start() / Awake()
    // 8. Update() / FixedUpdate()
    // 9. Public methods
    // 10. Private helper methods
    // 11. Unity callbacks (OnTriggerEnter2D, OnCollisionEnter2D, etc.)
    // 12. Coroutines
}
```

### Formatting
- Opening braces on new line for class/method declarations
- Single blank line between methods
- No commented-out code (cleaned during migration)
- Spaces around operators and after commas
- 4-space indentation

## Architectural Patterns

### Singleton Pattern
Every manager and key game object uses a lazy-loading singleton via `FindObjectOfType`:
```csharp
private static ClassName instance;

public static ClassName Instance
{
    get
    {
        if (instance == null)
        {
            instance = FindObjectOfType<ClassName>();
        }
        return instance;
    }
}
```

### Inheritance Hierarchy
- `Character` (abstract MonoBehaviour) serves as base for `Player` and `Enemy`
- Abstract members: `IsDead`, `TakeDamage()`, `Death()`
- Virtual members: `Start()`, `ChangeDirection()`, `ThrowKnife()`, `OnTriggerEnter2D()`
- Child classes call `base.Start()` and `base.Method()` for shared initialization

### State Pattern (Enemy AI)
```csharp
public interface IEnemyState
{
    void Enter(Enemy enemy);
    void Execute();
    void Exit();
    void OnTriggerEnter(Collider2D other);
}

// State transitions via:
enemy.ChangeState(new Idlestate());
enemy.ChangeState(new PatrolState());
```

### Event/Delegate Pattern
```csharp
public delegate void DeadEventHandler();
public event DeadEventHandler Dead;

// Subscription:
Player.Instance.Dead += new DeadEventHandler(RemoveTarget);

// Invocation:
if (Dead != null) { Dead(); }
```

## Common Implementation Patterns

### Unity Lifecycle Usage
- `Start()` for initialization, finding references via `FindObjectOfType<T>()`
- `FixedUpdate()` preferred over `Update()` for physics-related movement (Player, Enemy)
- `Update()` used for UI state management (Menu, PlayerStats)
- Coroutines (`IEnumerator`) for timed sequences (damage, immortality, knockback)

### Input Handling (Dual-mode via CrossPlatformInput)
Supports both keyboard and mobile touch input simultaneously:
```csharp
if (Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("jump"))
{
    MyAnimator.SetTrigger("jump");
}

float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
```

### Animation Control
Animator parameters driven from code:
```csharp
MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));
MyAnimator.SetBool("land", true);
MyAnimator.SetTrigger("attack");
MyAnimator.SetLayerWeight(1, 1);  // Layer blending for air/ground
```

### Persistence with PlayerPrefs
```csharp
// Save
PlayerPrefs.SetInt("levelReached", levelToUnlock);
PlayerPrefs.SetInt("Score", collectedCoins);

// Load
levelReached = PlayerPrefs.GetInt("levelReached", defaultValue);

// JSON save system
string json = JsonUtility.ToJson(states);
PlayerPrefs.SetString("save", json);
states = JsonUtility.FromJson<SaveState>(PlayerPrefs.GetString("save"));
```

### Tag-Based Collision Detection (using CompareTag)
```csharp
void OnCollisionEnter2D(Collision2D other)
{
    if (other.gameObject.CompareTag("Coin")) { ... }
    if (other.gameObject.CompareTag("Spikes")) { ... }
}
```

### Audio Playback
Centralized through AudioManager singleton with AudioMixer:
```csharp
audioManager.PlaySound("Knife Sound");
AudioManager.instance.SetSFXVolume(sfxLv);
AudioManager.instance.SetMusicVolume(musicLv);
```

### Menu State Machine
Enum-based UI state management in Update loop:
```csharp
public enum MenuStates { Playing, Pause, Options, Help, Completed }

void Update()
{
    switch (currentState)
    {
        case MenuStates.Playing:
            Time.timeScale = 1;
            break;
        case MenuStates.Pause:
            Time.timeScale = 0;
            break;
    }
}
```

## Practices & Conventions

### Serialization
- Use `[SerializeField]` for inspector-exposed private fields
- Public fields also used for inspector access (less encapsulated)
- Properties with `{ get; set; }` or `{ get; private set; }` for controlled access
- `[System.Serializable]` on data classes (SaveState, ColorToPrefab, Sound)

### Scene Management
- Scenes loaded by build index: `SceneManager.LoadScene(x)`
- Scene fader used for transitions: `fader.FadeTo(levelnumber)`
- Level progression tracked via PlayerPrefs `"levelReached"` key

### Health/Stat System
- Custom `stat` type with `CurrentVal`, `MaxVal`, and `Initialize()` method
- Damage dealt by subtracting from `healthStat.CurrentVal`
- Death triggered when `CurrentVal <= 0`

### Object Instantiation
```csharp
GameObject tmp = Instantiate(prefab, position, rotation);
tmp.GetComponent<ComponentType>().Initialize(params);
```

### Physics Interaction
- `Physics2D.IgnoreCollision()` for selective collision filtering
- `Rigidbody2D.AddForce()` for jumps and knockback
- `Physics2D.OverlapCircleAll()` for ground detection
- Layer-based ground checking with `LayerMask`

## Migration Notes
- All `using UnityStandardAssets.CrossPlatformInput;` removed — classes now in global namespace
- `transform.FindChild()` replaced with `transform.Find()` throughout
- All `.tag == "X"` replaced with `.CompareTag("X")` for GC optimization
- Redundant `(GameObject)` casts on `Instantiate` removed
- `SaveManager` now has working JSON serialization via `JsonUtility`
- `admobdemo.cs` and old `GoogleMobileAds/` folder pending removal (need new SDK first)
