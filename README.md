## Developer & Contributions

Muhammad Rafi R (Game Developer)
  <br>

## About

Out of the Box is a 2D puzzle-platformer where you play as a fox and crow duo. You can switch between the fox and crow to get through levels, each bringing their own strengths to help you along the way.The game mixes puzzle-solving and platforming, pushing you to figure out how to use both characters together to progress.
<br>

## Key Features

- **Character Switching** — Swap between the fox and the crow anytime to tackle puzzles and platforming challenges.  
- **Unique Abilities**  
  - **Fox:** double jump, push & pull boxes to clear paths or reach new areas.  
  - **Crow:** fly and carry boxes to higher places (some heavy boxes are too heavy for the crow).  
- **Dynamic Levels** — Reach a trigger point and the level flips/turns, changing its shape and look and revealing new routes and puzzles.  
- **Puzzle & Platforming Mix** — Thoughtful puzzles combined with skill-based platforming.  
- **Teamwork Gameplay** — Combine both characters' abilities to solve puzzles and progress.

<table>
  <tr>
    <td align="left" width="50%">
      <img width="100%" alt="gif1" src="https://github.com/Justsomeguy241/Justsomeguy241/blob/main/Fox.gif">
    </td>
  </tr>
</table>

## Scene Flow 

```mermaid
flowchart LR
  mm[Main Menu]
  gp[Gameplay]
  vs[Victory Screen]
  pm[Pause Menu]

  mm -- "Click Play" --> gp
  gp -- "Finish Level" --> vs
  vs -- "Main Menu" --> mm
  gp -- "Pause" --> pm
  pm -- "Resume" --> gp
   

```
## Layer / Module Design 

```mermaid
graph TD
    %% Initialization & Menus
    Start([Game Start])
    Boot[Boot Layer]
    MM[Main Menu]
    Settings[Settings]
    LevelSelect[Level Select]

    %% Gameplay Core
    GP[Gameplay Scene]
    Switch[Character Switching System]
    Puzzle[Puzzle & Physics System]
    Platforming[Platforming System]
    Flip[Level Flip System]

    %% Characters
    Fox[Fox Abilities<br/>Double Jump, Push/Pull]
    Crow[Crow Abilities<br/>Fly, Carry Boxes]

    %% UI & States
    Pause[Pause Menu]
    Victory[Victory Screen]

    %% Level Progression
    Tutorial[Tutorial Level]
    Level1[Level 1]
    Level2[Level 2]
    Level3[Level 3]

    %% Flows
    Start --> Boot --> MM
    MM -->|Settings| Settings
    MM -->|Level Select| LevelSelect
    Settings --> MM

    %% Level Select leads to any level
    LevelSelect --> Tutorial
    LevelSelect --> Level1
    LevelSelect --> Level2
    LevelSelect --> Level3

    %% Levels connect to Gameplay
    Tutorial --> GP
    Level1 --> GP
    Level2 --> GP
    Level3 --> GP
    GP --> Victory
    Victory --> LevelSelect

    %% Gameplay Systems
    GP --> Switch
    Switch --> Fox
    Switch --> Crow
    GP --> Puzzle
    GP --> Platforming
    GP --> Flip

    %% Pause Menu
    GP --> Pause
    Pause -->|Resume| GP
    Pause -->|Main Menu| MM


```


## Modules and Features

The advanced 2D platformer mechanics including progressive ability unlocks, teleportation system, level management, and dynamic audio are powered by a comprehensive scripting system that creates a unique gameplay experience.

| 📂 Name                    | 🎬 Scene      | 📋 Responsibility                                                                                                                                       |
| -------------------------- | ------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **GameManager**            | **Gameplay**  | - Oversees game state (playing, paused, level complete)<br/>- Connects systems like level completion and transitions                                    |
| **AudioManager**           | **Global**    | - Handles background music (OST) and sound effects (SFX)<br/>- Controls volume and muting                                                               |
| **MainMenu**               | **Main Menu** | - Displays the main menu UI<br/>- Lets player choose Tutorial, Level 1, Level 2, or Level 3<br/>- Provides buttons for starting, settings, and quitting |
| **OptionsPanel**           | **Main Menu** | - Adjusts audio settings<br/>- Applies user preferences                                                                                                 |
| **Levels Panel**           | **Main Menu** | - Level selection screen<br/>- Directly loads the chosen level                                                                                          |
| **Canvas / UI (Gameplay)** | **Gameplay**  | - Displays HUD elements<br/>- Shows panels like **LevelCompletePanel** and Pause Menu                                                                   |
| **Rotator (Flip System)**  | **Gameplay**  | - Handles level flipping/rotation<br/>- Dynamically changes the layout and reveals new routes                                                           |
| **Puzzle Objects (Grid)**  | **Gameplay**  | - Contains puzzle elements:<br/>PopupWalls, Levers, Buttons, Doors<br/>- Reacts to player interactions to unlock paths                                  |
| **LargeMoveableBox**       | **Gameplay**  | - Heavy interactable object<br/>- Can be pushed/pulled by the Fox<br/>- Too heavy for the Crow                                                          |
| **SmallTrash**             | **Gameplay**  | - Light objects<br/>- Can be carried by the Crow                                                                                                        |
| **Fox**                    | **Gameplay**  | - Playable character<br/>- Wall jump, push, pull objects                                                                                              |
| **Crow**                   | **Gameplay**  | - Playable character<br/>- Fly and carry light objects                                                                                                  |
| **FoxAndCrowDetector**     | **Gameplay**  | - Detects when both characters reach the exit<br/>- Triggers level completion                                                                           |
| **ExitDoorR**              | **Gameplay**  | - Level exit<br/>- Works with FoxAndCrowDetector to trigger victory state                                                                               |




<br>


## Game Flow Chart


```mermaid
---
config:
  theme: redux
  look: neo
---
flowchart TD
  start([Game Start])
  start --> menu{Main Menu}
  menu -->|"Select Level"| level[Load Chosen Level]
  menu -->|"Settings"| settings[Adjust Audio/Preferences]
  menu -->|"Quit"| end([Exit Game])
  settings --> menu

  %% Gameplay Core
  level --> input{Player Input}
  input -->|"Move / Jump"| move[Apply Movement & Physics]
  input -->|"Switch Character"| switch[Switch Between Fox & Crow]

  %% Character Abilities
  switch --> foxFox[Fox: Double Jump, Push/Pull Box]
  switch --> crowCrow[Crow: Fly, Carry Light Box]

  %% Puzzles
  move --> puzzle{Puzzle Interaction}
  foxFox --> puzzle
  crowCrow --> puzzle

  puzzle --> lever[Levers / Buttons / Doors]
  puzzle --> box[Moveable Boxes]
  puzzle --> wall[Popup Walls / Obstacles]

  %% Flip Mechanic
  move --> flip{Flip Triggered?}
  flip -->|Yes| doFlip[Rotate/Flip Level Layout]
  flip -->|No| cont1[Continue Gameplay]

  %% Exit System
  cont1 --> exitChk{Both Fox & Crow at Exit?}
  doFlip --> exitChk

  exitChk -->|Yes| complete[Level Complete Panel]
  exitChk -->|No| input

  complete --> backToSelect[Return to Level Select]
  backToSelect --> menu


```


<br>

## Event Signal Diagram


```mermaid
classDiagram
    %% --- Core Gameplay ---
    class PlayerController {
        +OnJump()
        +OnDash()
        +OnSlide()
        +OnWallJump()
        +OnAbilityUnlocked(abilityName: string)
    }

    class TeleportManager {
        +OnTeleportStart()
        +OnTeleportComplete()
    }

    class Teleporter {
        +OnPlayerEnter()
        +OnPlayerExit()
    }

    class MovingTeleporter {
        +OnReachWaypoint(index: int)
    }

    class DirectionalBooster {
        +OnBoostApplied(direction: vector2, force: float)
    }

    %% --- Systems ---
    class GameManager {
        +OnLevelStart(levelName: string)
        +OnLevelComplete(levelName: string)
        +OnPlayerDeath()
    }

    class AudioManager {
        +OnPlayBGM(trackName: string)
        +OnPlaySFX(effectName: string)
    }

    class SaveSystem {
        +OnSave(slot: int)
        +OnLoad(slot: int)
    }

    %% --- Relations (who emits what) ---
    PlayerController --> TeleportManager : emits
    PlayerController --> DirectionalBooster : triggers
    TeleportManager --> Teleporter : controls
    GameManager --> SaveSystem : calls
    GameManager --> AudioManager : triggers



```


<br>





<br>

## Play The Game

<a href="#">Play Now</a>
<br>




![Platform Demo](https://raw.githubusercontent.com/adxze/adxze/main/PlatfromSlide.png)
