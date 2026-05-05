# Product Overview - Impossible Ninja (2D Ninja Platformer)

## Purpose
A 2D side-scrolling ninja platformer game built in Unity, featuring combat mechanics, multiple levels, and mobile deployment with ad monetization.

## Key Features
- **Player Combat**: Melee sword attacks and ranged knife throwing
- **Enemy AI**: State-machine driven enemies (idle, patrol, melee, ranged states)
- **Multi-Level Progression**: 5+ levels with a level selection system
- **Health System**: Stat-based health with visual health bar UI
- **Save System**: Persistent player progress via PlayerPrefs + JSON serialization
- **Dialogue System**: Text-based NPC/story dialogue with text file imports
- **Parallax Scrolling**: Multi-layer background parallax effects
- **Moving Platforms**: Dynamic platform movement mechanics
- **Camera Follow**: Smooth camera tracking of the player
- **Scene Transitions**: Fade-based scene transitions between levels
- **Shop System**: In-game shop (likely cosmetics or upgrades)
- **Ad Monetization**: Google AdMob integration (pending SDK update for Unity 6)
- **Audio Management**: Centralized audio/music management with mixer support
- **Level Generation**: Color-to-prefab procedural level building from images

## Target Platform
- Mobile (Android) - evidenced by keystore files, Google Ads SDK, and Play Services Resolver
- Potential desktop builds

## Target Users
- Casual mobile gamers who enjoy action platformers
- Players who like ninja/combat themed side-scrollers

## Monetization
- Google Mobile Ads (AdMob) integration — needs SDK update for Unity 6
- Unity Ads SDK (v4.4.2)

## Migration Status
- **Phase 1 COMPLETE**: Code cleanup (dead code removed, deprecated APIs fixed)
- **Phase 2 COMPLETE**: CrossPlatformInput namespace removed (drop-in replacement created)
- **Phase 3 PENDING**: Unity version upgrade (2020.3 → 2022.3 → Unity 6)
- **Phase 4 PENDING**: AdMob SDK update for new Google Mobile Ads API
- **Phase 5 PENDING**: Android build configuration for Play Store
