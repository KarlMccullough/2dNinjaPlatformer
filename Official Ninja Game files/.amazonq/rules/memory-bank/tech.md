# Technology Stack

## Engine & Runtime
- **Game Engine**: Unity 2020.3.49f1 (LTS) — MIGRATING TO Unity 6
- **Scripting Language**: C# (.NET Standard 2.0 / .NET 4.x equivalent)
- **Rendering**: Unity 2D Renderer (Sprite-based)

## Migration Path
```
Unity 2020.3.49f1 → Unity 2022.3 LTS → Unity 6
```

### Migration Status
| Step | Status | Notes |
|------|--------|-------|
| Code cleanup (dead code, deprecated APIs) | ✅ Done | All scripts cleaned |
| `transform.FindChild()` → `transform.Find()` | ✅ Done | Enemy.cs |
| `.tag ==` → `.CompareTag()` | ✅ Done | All collision scripts |
| Remove `(GameObject)` Instantiate cast | ✅ Done | Character.cs, Enemy.cs |
| CrossPlatformInput namespace removal | ✅ Done | Drop-in replacement created |
| `using UnityStandardAssets.CrossPlatformInput` removed | ✅ Done | Player.cs, Menu.cs, TextBoxManager.cs |
| AdManager old SDK references removed | ✅ Done | Placeholder ready for new SDK |
| SaveManager Save/Load implemented | ✅ Done | JSON via PlayerPrefs |
| Unity 2022.3 upgrade | ⬜ Pending | Open in Unity Hub |
| Unity 6 upgrade | ⬜ Pending | After 2022.3 stable |
| New Google Mobile Ads SDK | ⬜ Pending | v9.x+ required |
| Android build config (API 34, IL2CPP, ARM64) | ⬜ Pending | Play Store requirements |

## Unity Packages (from manifest.json)
| Package | Version | Purpose |
|---------|---------|---------|
| com.unity.ads | 4.4.2 | Unity Ads monetization |
| com.unity.textmeshpro | 3.0.6 | Advanced text rendering |
| com.unity.timeline | 1.4.8 | Timeline/cutscene system |
| com.unity.ugui | 1.0.0 | Unity UI system |
| com.unity.modules.physics2d | 1.0.0 | 2D physics (Rigidbody2D, Collider2D) |
| com.unity.modules.animation | 1.0.0 | Animation system |
| com.unity.modules.audio | 1.0.0 | Audio playback |
| com.unity.modules.tilemap | 1.0.0 | Tilemap level design |
| com.unity.modules.ai | 1.0.0 | Navigation/AI |

## Third-Party SDKs
- **Google Mobile Ads (AdMob)** - TO BE ADDED (old SDK fully removed, placeholder AdManager.cs ready)
- **External Dependency Manager (EDM4U)** - TO BE ADDED when new AdMob SDK is integrated

## Target Platform
- **Primary**: Android (multiple .keystore signing files present)
- **Build System**: Unity Build Pipeline
- **IDE Support**: Visual Studio, VS Code, JetBrains Rider

## Key Unity Systems Used
- Animator / Animation Controllers (character animations)
- Physics2D (Rigidbody2D, Collider2D, EdgeCollider2D)
- SceneManagement (multi-scene level loading by build index)
- Coroutines (async game logic)
- PlayerPrefs + JSON serialization (persistence)
- UI Canvas system (menus, HUD)
- TextMeshPro (UI text)
- Tilemaps (level design)
- AudioMixer (volume control for Master, SFX, Music)

## Development Commands
```bash
# Open project in Unity
# Use Unity Hub → Open Project → select "Official Ninja Game files" folder

# Build Android
# Unity Editor → File → Build Settings → Android → Build

# Run in Editor
# Unity Editor → Play button (Ctrl+P / Cmd+P)
```

## Project Configuration
- Solution file: `Official Ninja Game files.sln`
- Main assembly: `Assembly-CSharp.csproj`
- Editor assembly: `Assembly-CSharp-Editor.csproj`

## Removed During Migration
- `Assets/admobdemo.cs` — Old AdMob demo ✅ Removed
- `Assets/GoogleMobileAds/` — Old SDK folder ✅ Removed
- `Assets/PlayServicesResolver/` — Old resolver ✅ Removed
- `Assets/Plugins/` — Old AdMob .aar/.jar dependencies ✅ Removed
- `Assets/TextMesh Pro/Examples & Extras/` — Incompatible with Unity 6 ✅ Removed
- `ProjectSettings/GvhProjectSettings.xml` — Old resolver config ✅ Removed
- `ProjectSettings/AndroidResolverDependencies.xml` — Old resolver config ✅ Removed
