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

The advanced 2D platformer systems — including character switching, puzzle interactions, level progression, and dynamic audio — are powered by a modular scripting structure.
Each module is responsible for managing specific aspects of gameplay, UI, or global systems to ensure smooth transitions and responsive player control.

| 📂 **Name**                 | 🎬 **Scene / Scope** | 📋 **Responsibility**                                                                                                                                    |
| --------------------------- | -------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **GameManager**             | **Gameplay**         | - Controls overall game state (playing, paused, level complete, failed)<br/>- Coordinates transitions between menus, levels, and victory screens         |
| **AudioManager**            | **Global**           | - Manages background music (OST) and sound effects (SFX)<br/>- Handles audio settings such as volume and mute controls                                   |
| **MainMenu**                | **Main Menu**        | - Displays the main menu interface<br/>- Provides access to level selection, settings, and exit options                                                  |
| **OptionsPanel**            | **Main Menu**        | - Adjusts and applies user preferences (e.g., audio volume)<br/>- Saves settings for future sessions                                                     |
| **LevelsPanel**             | **Main Menu**        | - Handles level selection<br/>- Loads the selected scene or level into gameplay                                                                          |
| **Canvas / UI (Gameplay)**  | **Gameplay**         | - Manages on-screen UI elements such as HUD, pause menu, and level complete screens<br/>- Displays player feedback and progress indicators               |
| **Rotator (Flip System)**   | **Gameplay**         | - Handles level rotation mechanics<br/>- Dynamically alters the environment to reveal new paths or puzzle solutions                                      |
| **Puzzle Objects (Grid)**   | **Gameplay**         | - Manages interactable puzzle elements (e.g., levers, doors, buttons, pop-up walls)<br/>- Responds to player interactions and triggers logical responses |
| **LargeMoveableBox**        | **Gameplay**         | - Heavy interactable object<br/>- Can be pushed or pulled by the **Fox** but not lifted<br/>- Used in environmental puzzles                              |
| **SmallTrash**              | **Gameplay**         | - Lightweight object<br/>- Can be picked up and carried by the **Crow** to solve certain puzzles                                                         |
| **Fox**                     | **Gameplay**         | - Playable character focused on strength-based actions<br/>- Can wall-jump, push, and pull heavy objects                                                 |
| **Crow**                    | **Gameplay**         | - Playable character focused on agility<br/>- Can fly and carry light objects to assist in puzzle-solving                                                |
| **Character Switch System** | **Gameplay**         | - Handles input-based switching between the Fox and the Crow<br/>- Ensures smooth transitions while retaining control context                            |
| **FoxAndCrowDetector**      | **Gameplay**         | - Detects when both Fox and Crow reach the exit area<br/>- Triggers level completion event through **GameManager**                                       |
| **ExitDoorR**               | **Gameplay**         | - Represents the exit point of each level<br/>- Works with **FoxAndCrowDetector** to activate victory conditions                                         |





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

    class AudioManager {
        +OnPlayBGM(trackName: string)
        +OnPlaySFX(effectName: string)
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
