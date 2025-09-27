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

| 📂 Name              | 🎬 Scene                       | 📋 Responsibility                                                                                                               |
| -------------------- | ------------------------------ | ------------------------------------------------------------------------------------------------------------------------------- |
| **MainMenu**         | **Main Menu**                  | - Show main menu UI<br/>- Allow player to select Tutorial, Level 1, Level 2, or Level 3<br/>- Exit game when player quits       |
| **Settings**         | **Main Menu**<br/>**Gameplay** | - Show settings menu (UI)<br/>- Configure audio volume<br/>- Adjust basic game preferences                                      |
| **AudioSystem**      | **Main Menu**<br/>**Gameplay** | - Play background music & sound effects<br/>- Adjust or mute audio based on settings                                            |
| **PlayerController** | **Gameplay**                   | - Handle movement & physics for both characters<br/>- Manage input for Fox and Crow<br/>- Enable switching between Fox and Crow |
| **FoxModule**        | **Gameplay**                   | - Allow double jump<br/>- Push & pull boxes                                                                                     |
| **CrowModule**       | **Gameplay**                   | - Enable flight<br/>- Carry boxes to higher platforms (except heavy boxes)                                                      |
| **PuzzleSystem**     | **Gameplay**                   | - Manage box interactions and puzzle logic<br/>- Detect puzzle completion triggers                                              |
| **FlipSystem**       | **Gameplay**                   | - Trigger level flip/rotation events<br/>- Change layout and open new routes                                                    |
| **LevelManager**     | **Gameplay**                   | - Handle level loading (Tutorial, L1, L2, L3)<br/>- Manage transitions between levels<br/>- Track level completion              |
| **PauseMenu**        | **Gameplay**                   | - Show pause menu<br/>- Resume gameplay or return to main menu                                                                  |
| **VictoryScreen**    | **Victory Screen**             | - Display when a level is completed<br/>- Allow return to Main Menu or Level Select                                             |



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
  start --> input{Player input}
  input -->|"Move or Jump"| move[Apply physics movement]
  move --> abil{Ability unlocked}
  abil -->|Yes| doAbility[Use ability]
  abil -->|No| cont1[Continue]
  input -->|"Teleport key J"| tpChk[Check teleporters]
  tpChk --> tpOK{Teleporter valid}
  tpOK -->|Yes| doTp[Teleport player]
  tpOK -->|No| cont2[Continue]
  move --> hitBoost{Hit booster}
  hitBoost -->|Yes| doBoost[Apply booster force]
  hitBoost -->|No| cont3[Continue]
  doAbility --> loop[Continue loop]
  doTp --> loop
  doBoost --> loop
  cont1 --> collide{End reached or hazard}
  cont2 --> collide
  cont3 --> collide
  loop --> collide
  collide -->|Hazard| respawn[Respawn at checkpoint]
  collide -->|Level end| save[Save progress]
  respawn --> start
  save --> next[Load next level]
  next --> start


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
