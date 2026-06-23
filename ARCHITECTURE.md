# Architecture

## Overview

The project follows a modular, manager-based architecture where each system is responsible for a single aspect of the game. Gameplay logic is separated from card presentation, allowing core mechanics such as matching, scoring, saving, and level progression to remain independent of the visual implementation.

## Main Components

### GameInitializer

Acts as the entry point for a game session.

Responsibilities:
- Loads saved progress (if available)
- Requests board generation
- Initializes gameplay systems
- Restores score and progression
- Shows the preview phase for a new game
- Saves progress on application pause/quit and after successful matches

### GridSpawner

Responsible for creating the game board.

Responsibilities:
- Supports all required grid layouts through `GridConfigSO`
- Generates card pairs
- Performs deterministic seeded shuffling
- Calculates responsive card size based on available screen space
- Restores board state when loading a saved game

### MatchHandler

Owns the core gameplay logic.

Responsibilities:
- Registers every card
- Receives card click events
- Maintains a queue of flipped cards
- Processes pairs asynchronously
- Detects matches and mismatches
- Prevents invalid interactions such as matching already matched or already face-up cards
- Raises an event when a pair has been evaluated

Using a queue allows players to continue selecting cards while previous comparisons are still resolving, satisfying the continuous input requirement without locking the entire board.

### Card & CardController

The visual card (`Card`) is responsible only for presentation:
- Flip animation
- Match animation
- Visual state
- Forwarding click events

`CardController` acts as a lightweight wrapper that associates a `CardDataSO` with its corresponding `Card` instance while tracking gameplay state such as whether the card has already been matched.

This keeps gameplay logic separate from rendering behaviour.

### LevelManager

Responsible for overall game progression.

Responsibilities:
- Tracks moves
- Tracks matched pairs
- Detects level completion
- Detects level failure
- Raises game-over events
- Clears save data when a game finishes

### ScoreSystem

Handles all scoring independently from the match logic.

Responsibilities:
- Listens for pair evaluation events
- Awards points for successful matches
- Applies combo bonuses
- Resets combo streaks after mismatches
- Broadcasts score updates to the UI

### SaveSystem

Provides persistence using PlayerPrefs.

The save contains:
- Board layout (card indices)
- Matched card states
- Current score
- Number of matched pairs
- Number of moves

Saving only the data required to reconstruct gameplay keeps the save format compact while restoring a consistent game state.

### AudioManager

A singleton responsible for playing gameplay sound effects including:
- Card flip
- Match
- Mismatch
- Victory
- Defeat

## Data Flow

```
Player Input
      │
      ▼
InputManager
      │
      ▼
Card
      │
(OnClicked)
      │
      ▼
MatchHandler
      │
      ├────────► ScoreSystem
      │
      ├────────► LevelManager
      │
      └────────► GameInitializer (save progress)
                     │
                     ▼
                 SaveSystem
```

## Data Assets

The project uses ScriptableObjects for configuration and content.

### CardDataSO

Stores the visual data for each card:
- Front sprite
- Base sprite

### GridConfigSO

Defines the board configuration:
- Rows
- Columns
- Spacing
- Padding

Using ScriptableObjects allows layouts and card content to be modified without changing code.

## Design Decisions

The game uses an event-driven approach for communication between gameplay systems. `MatchHandler` exposes an `OnPairEvaluated` event that is consumed by `ScoreSystem`, `LevelManager`, and `GameInitializer`. This reduces coupling between systems and allows each manager to respond independently when gameplay events occur.

The `Card` class is intentionally focused on presentation while gameplay state is maintained by `CardController`, keeping rendering concerns separate from game rules.

## Trade-off

To keep the prototype focused, the project uses a manager-based architecture with direct references assigned through the Unity Inspector instead of introducing dependency injection or a more complex service architecture.

For a larger production project, I would replace some of these direct references with dependency injection or an event bus to improve scalability and testability, but I felt the current approach provided a good balance between simplicity, readability, and maintainability for the scope of this assessment.
