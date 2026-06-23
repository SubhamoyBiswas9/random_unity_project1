# Development Log

## Project Goal

Implement a small but robust memory card game while focusing on correctness, maintainability, and handling edge cases over visual polish.

## Key Decisions

### Separation of Gameplay Logic

During development I refactored the core gameplay rules into standalone C# classes such as `MatchingService`, `ScoreCalculator`, and `ShuffleUtility`. This separates business logic from Unity-specific behaviour, making the systems easier to understand, maintain, and unit test.

### Input Handling

One of the biggest requirements was allowing continuous player input while previous matches were still resolving. Instead of locking the entire board during comparisons, each card manages its own interaction state while the match system queues and validates comparisons independently.

This prevents duplicate matches, invalid selections, and cards becoming stuck in incorrect states.

### Deterministic Shuffle

The board uses a seeded random generator so the same seed always produces the same card arrangement.

### Save System

The save file stores information using json in PlayerPrefs to restore a consistent gameplay state after restarting the application, including card states, score, and current progress.

### Unit Testing

To improve reliability, I extracted the core game logic into testable classes and added Unity EditMode unit tests covering matching, scoring, deterministic shuffling, and save/load functionality. This helped verify gameplay rules independently from the Unity runtime.

## Approach I Abandoned

Initially I considered locking the entire board whenever two cards were selected until the comparison animation completed.

Although simpler, this directly violated the assignment requirement for continuous input. I replaced it with independent card state handling and asynchronous match resolution, allowing players to continue interacting without breaking game state consistency.

## If I Had More Time

- Better visual polish
- Additional sound effects
- More polished UI transitions
