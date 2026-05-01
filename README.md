# Unity-Horror-Game-by-Georgios-Iasonidis

[Forgiveness_README.md](https://github.com/user-attachments/files/27273061/Forgiveness_README.md)
# Forgiveness

**Forgiveness** is a psychological horror game developed in **Unity**.  
The project focuses on atmosphere, tension, environmental storytelling, and player immersion through scripted interactions, animations, lighting effects, sound, and exploration.

## Game Overview

The game is designed as a psychological horror experience where the player explores a disturbing environment and interacts with objects, doors, notes, lighting elements, and gameplay triggers. The overall goal of the project is to create suspense and unease through level design, audio, animation systems, and scripted events.

## Features

- First-person player movement
- Camera control system
- Flashlight interaction and pickup system
- Door interaction and trigger-based events
- Flickering light effects for horror atmosphere
- Main menu and pause menu systems
- Note pickup / interaction system
- Teleport or trigger controller logic
- Animation-based interactions for doors, drawers, and environment objects
- Environmental assets, sound effects, and animations

## Project Structure

```text
Assets/
├── ANIMATIONS/
│   ├── DoorOpen.anim
│   ├── DoorClose.anim
│   ├── DoorOpen_toilet.anim
│   ├── DoorClose_toilet.anim
│   ├── Main_Doors.controller
│   ├── Toilet_Door.controller
│   ├── NearClockDrawer/
│   ├── NearCouchDrawer/
│   ├── Toilet/
│   ├── UnderJesusPickDrawer/
│   ├── DrawerToForward1/
│   ├── DrawerToForward2/
│   ├── DrawerToForward3/
│   ├── DrawerToForward4/
│   ├── DrawerToLeft/
│   ├── DrawerToRight/
│   ├── DrawerForward1/
│   ├── DrawerForward2/
│   ├── courtineToilet/
│   └── ToiletDrawer/
├── Flashlight/
├── Player/
├── Scenes/
├── SCRIPTS/
│   ├── FlashLight/
│   │   └── FlashLightPickUp.cs
│   ├── Player/
│   │   ├── MoveCam.cs
│   │   ├── PlayerCam.cs
│   │   └── PlayerMovement.cs
│   ├── Doorbehavior.cs
│   ├── DoorTriggering.cs
│   ├── Flickering.cs
│   ├── footstepsController.cs
│   ├── KeepPlayerOrientation.cs
│   ├── KeppFlashOnHand.cs
│   ├── LightLampON_OFF.cs
│   ├── MainMenu.cs
│   ├── NotePickUP.cs
│   ├── PauseMenu.cs
│   └── TpController.cs
├── SoundEffects/
├── TextMesh Pro/
├── Textures/
└── other environment / prop asset folders
```

## Technologies Used

- **Unity**
- **C#**
- **TextMesh Pro**
- Animation controllers and `.anim` clips
- Audio effects
- Environment and prop assets
- Scripted gameplay events

## Gameplay Systems

The project includes several gameplay systems implemented through C# scripts and Unity animation assets.

### Player Systems
- `PlayerMovement.cs` handles player movement.
- `PlayerCam.cs` and `MoveCam.cs` manage camera behavior and viewing control.

### Interaction Systems
- `FlashLightPickUp.cs` supports flashlight pickup behavior.
- `Doorbehavior.cs` and `DoorTriggering.cs` control door interaction and trigger-based events.
- `NotePickUP.cs` supports collectible or readable note interactions.

### Animation Systems
- The `ANIMATIONS` folder contains animation clips and controllers for object interactions such as doors, toilet doors, drawers, curtains, and nearby environment objects.
- Files such as `DoorOpen.anim`, `DoorClose.anim`, `DoorOpen_toilet.anim`, and `DoorClose_toilet.anim` indicate animated environmental interactions.
- Controllers such as `Main_Doors.controller` and `Toilet_Door.controller` are used to manage animation state transitions.
- Additional animation folders such as `DrawerToForward1`, `DrawerToLeft`, `DrawerToRight`, `NearClockDrawer`, `NearCouchDrawer`, `Toilet`, and `UnderJesusPickDrawer` suggest multiple animated interaction points in the environment.

### Atmosphere Systems
- `Flickering.cs` and `LightLampON_OFF.cs` contribute to lighting-based horror effects.
- `footstepsController.cs` supports movement-related audio feedback.
- `MainMenu.cs` and `PauseMenu.cs` manage UI flow and game state transitions.

## Purpose

This project was created as an educational Unity horror game project, with a focus on:

- Psychological horror design
- Unity scene building
- C# gameplay scripting
- Animation-based interaction systems
- Environmental storytelling
- Audio-visual atmosphere creation
- Interactive first-person gameplay

## Media

- [YouTube Gameplay](https://www.youtube.com/watch?v=IRGKdujADSM)
- [itch.io Project Page](https://giorgosiasonidis.itch.io/forgiveness)

## Notes

- This project is presented as an educational Unity game project.
- The game is available on itch.io.
- The repository may contain scripts, animations, scenes, audio, and supporting Unity resources based on the current project structure.

## Author

Developed by **GiorgosIasonidis**.
