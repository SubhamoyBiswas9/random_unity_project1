# Architecture

## Overview

The project follows a modular, manager-based architecture where Unity-specific behaviour is separated from core gameplay logic. Systems communicate primarily through events, while reusable game rules are implemented as standalone C# classes that can be tested independently using Unity EditMode tests.

## Main Components

### GameInitializer

Acts as the entry point for a game session.

Responsibilities:
- Loads saved progress
- Generates the board
- Initializes gameplay systems
- Restores score and progression
- Displays the preview phase for a new game
- Saves progress on application pause, quit, and successful matches

### GridSpawner

Responsible for generating the board.

Responsibilities:
- Supports multiple grid layouts through `GridConfigSO`
- Generates matching card pairs
- Performs deterministic seeded shuffling using `ShuffleUtility`
- Calculates responsive card scaling based on available screen space
- Restores board state from saved data

### MatchHandler

Coordinates gameplay interactions.

Responsibilities:
- Registers cards
- Receives player input events
- Queues flipped cards
- Resolves comparisons asynchronously
- Plays match and mismatch animations
- Raises events when a pair has been evaluated

The matching decision itself is delegated to `MatchingService`, allowing the gameplay rule to remain independent from Unity components.

### Card

Represents the visual behaviour of a card.

Responsibilities:
- Flip animation
- Match animation
- Visual state
- Click forwarding

The class contains presentation logic only and does not determine whether two cards form a valid match.

### CardController

Associates a `Card` with its corresponding `CardDataSO` while tracking gameplay state such as whether the card has already been matched.

### LevelManager

Tracks overall game progression.

Responsibilities:
- Move count
- Match progress
- Win detection
- Lose detection
- Clearing saved progress after game completion

### ScoreSystem

Acts as a bridge between gameplay events and the scoring logic.

Responsibilities:
- Listens for pair evaluation events
- Updates score through `ScoreCalculator`
- Broadcasts score updates to the UI

### SaveSystem

Provides persistence using PlayerPrefs.

Stored data includes:
- Board layout
- Matched cards
- Current score
- Moves
- Matched pairs

### AudioManager

Responsible for gameplay sound effects.

## Core Logic Classes

### MatchingService

A pure C# class responsible only for determining whether two cards form a valid match.

### ScoreCalculator

Contains all scoring and combo calculations independently of Unity.

### ShuffleUtility

Implements deterministic seeded shuffling as a reusable utility.

These classes contain no MonoBehaviour dependencies, making them suitable for fast EditMode unit testing.

## Data Assets

### CardDataSO

Stores the visual data associated with each card.

### GridConfigSO

Defines:
- Rows
- Columns
- Card spacing
- Padding

Using ScriptableObjects keeps gameplay configuration data separate from code.

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
      ▼
MatchHandler
      │
      ▼
MatchingService
      │
      ▼
OnPairEvaluated Event
      │
      ├────────► ScoreSystem
      │              │
      │              ▼
      │       ScoreCalculator
      │
      ├────────► LevelManager
      │
      └────────► GameInitializer
                     │
                     ▼
                 SaveSystem
```

## Design Decisions

Gameplay rules were intentionally separated from MonoBehaviour classes wherever practical. This improves maintainability, allows the core systems to be tested independently, and reduces coupling between gameplay logic and Unity presentation code.

Communication between managers is primarily event-driven, reducing direct dependencies between gameplay systems.

## Trade-off

For this prototype I chose a manager-based architecture with inspector references instead of introducing dependency injection or a more complex service architecture.

Given more development time, I would explore dependency injection and a more event-driven architecture to further improve scalability and automated testing.
