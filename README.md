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
  ls[Level Select]

  mm -- "Click Play" --> ls
  gp -- "Finish Level" --> vs
  vs -- "Main Menu" --> mm
  gp -- "Pause" --> pm
  pm -- "Resume" --> gp
  ls -- "Select Level" --> gp
  ls -- "Back" --> mm
  pm -- "Main Menu" --> mm

```
## Layer / Module Design 

```mermaid
---
config:
  theme: neutral
  look: neo
---
graph TD
    %% ====== INITIALIZATION ======
    subgraph "Initialization Phase"
        Start([Game Start])
        BootNote([Boot Sequence:<br/>Load Managers, Systems, and First Scene])
    end

    %% ====== CORE SYSTEMS ======
    subgraph "Core Systems"
        InputManager[Input Manager]
        AudioManager[Audio Manager]
        SceneLoader[Scene Loader]
        UIManager[UI Manager]
    end

    %% ====== GAMEPLAY ======
    subgraph "Gameplay Logic"
        GameManager[Game Manager]
        PlayerController[Player Controller]
        SwitchSystem[Character Switch System]
        Fox[Fox Controller]
        Crow[Crow Controller]
        PuzzleSystem[Puzzle / Physics System]
    end

    %% ====== USER INTERFACE ======
    subgraph "UI System"
        MainMenu[Main Menu]
        PauseMenu[Pause Menu]
        HUD[In-Game HUD]
        VictoryScreen[Victory / End Screen]
        Settings[Settings Menu]
    end

    %% ====== FLOW CONNECTIONS ======
    Start --> BootNote
    BootNote --> InputManager
    BootNote --> AudioManager
    BootNote --> SceneLoader
    BootNote --> UIManager

    %% Gameplay Links
    InputManager --> PlayerController
    PlayerController --> SwitchSystem
    SwitchSystem --> Fox
    SwitchSystem --> Crow
    PlayerController --> PuzzleSystem
    PuzzleSystem --> GameManager
    GameManager --> UIManager

    %% UI Links
    UIManager --> MainMenu
    UIManager --> PauseMenu
    UIManager --> HUD
    UIManager --> VictoryScreen
    UIManager --> Settings
    MainMenu --> GameManager
    PauseMenu --> GameManager
    HUD --> PlayerController
    VictoryScreen --> MainMenu

    %% Styling
    classDef initStyle fill:#e1f5fe,stroke:#01579b,stroke-width:2px
    classDef systemStyle fill:#ede7f6,stroke:#4a148c,stroke-width:2px
    classDef gameplayStyle fill:#e8f5e9,stroke:#1b5e20,stroke-width:2px
    classDef uiStyle fill:#fff3e0,stroke:#e65100,stroke-width:2px

    class Start,BootNote initStyle
    class InputManager,AudioManager,SceneLoader,UIManager systemStyle
    class GameManager,PlayerController,SwitchSystem,Fox,Crow,PuzzleSystem gameplayStyle
    class MainMenu,PauseMenu,HUD,VictoryScreen,Settings uiStyle

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
  %% --- Game Start & Menu ---
  start([Game Start])
  start --> menu{Main Menu}

  menu -->|"Select Level"| level[Load Chosen Level]
  menu -->|"Settings"| settings[Adjust Audio/Preferences]
  menu -->|"Quit"| quit([Exit Game])
  settings --> menu

  %% --- Gameplay Core ---
  level --> input{Player Input}
  input -->|"Move / Jump"| move[Apply Movement & Physics]
  input -->|"Switch Character"| switch[Switch Between Fox & Crow]

  %% --- Character Abilities ---
  switch --> fox[Fox: Wall Jump, Push/Pull Box]
  switch --> crow[Crow: Fly, Carry Light Box]

  %% --- Puzzle System ---
  move --> puzzle{Puzzle Interaction}
  fox --> puzzle
  crow --> puzzle

  puzzle --> lever[Levers / Buttons / Doors]
  puzzle --> box[Moveable Boxes]
  puzzle --> wall[Popup Walls / Obstacles]

  %% --- Flip Mechanic ---
  move --> flip{Flip Triggered?}
  flip -->|Yes| doFlip[Rotate/Flip Level Layout]
  flip -->|No| cont[Continue Gameplay]

  %% --- Exit System ---
  cont --> exitChk{Both Fox & Crow at Exit?}
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
        +Move(direction: Vector2)
        +Jump()
        +SwitchCharacter()
    }

    class Fox {
        +WallJump()
        +PushBox()
        +PullBox()
    }

    class Crow {
        +Fly()
        +CarryBox(weight: float)
    }

    class PuzzleSystem {
        +ActivateLever()
        +PressButton()
        +OpenDoor()
    }

    class Box {
        +Move()
        +IsCarryable(): bool
    }

    class FlipSystem {
        +TriggerFlip()
        +RotateLevel()
    }

    %% --- Game Flow & UI ---
    class GameManager {
        +StartLevel(levelName: string)
        +CompleteLevel()
    }

    class UIManager {
        +ShowPauseMenu()
        +ShowVictoryScreen()
    }

    %% --- Relations ---
    PlayerController --> Fox : controls
    PlayerController --> Crow : controls
    PlayerController --> PuzzleSystem : interacts
    Fox --> Box : pushes/pulls
    Crow --> Box : carries
    PuzzleSystem --> Box : requires
    FlipSystem --> PuzzleSystem : flips
    FlipSystem --> Fox : flips
    FlipSystem --> Crow : flips
    GameManager --> UIManager : manages
    GameManager --> AudioManager : triggers
    GameManager --> FlipSystem : activates


```


<br>
