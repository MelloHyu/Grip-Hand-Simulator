# Grip Says

Grip Says is a 3D interaction prototype built in Unity that explores alternative object interaction using five dedicated input keys. Instead of using a single interact button, players control each finger individually to form realistic hand gestures and pick up objects requiring different grips.

This project was developed as part of a Game Development Internship assignment.

---

## Gameplay

The player controls a 3D hand using the mouse and five dedicated finger keys.

A target object is displayed on screen. The player must:

1. Find the correct object.
2. Form the correct hand gesture.
3. Pick up the object.
4. Place it inside the assessment zone.
5. Hold it there until the assessment timer finishes.

Successfully assessed objects are collected, and the next target is displayed. Picking up or submitting the wrong object ends the game.

---

## Features

- Individual control of all five fingers
- Mouse-controlled 3D hand movement
- Wrist rotation using the middle mouse button
- Gesture recognition system
- Object-specific grip requirements
- Four unique interactable objects
- Assessment timer
- Collection animation
- Simon Says inspired gameplay loop

---

## Objects

| Object | Required Grip |
|---------|---------------|
| 🍎 Apple | Power Grip |
| 🪙 Coin | Precision Pinch |
| 🔑 Key | Tripod Grip |
| ☕ Mug | Handle Grip |

Each object requires a different finger combination, encouraging balanced use of all five input keys.

---

## Controls

### Hand Movement

| Action | Control |
|---------|----------|
| Move Hand | Mouse |
| Raise / Lower Hand | Mouse Wheel |
| Rotate Wrist | Hold Middle Mouse + Move Mouse |

### Finger Controls

Replace the keys below with the ones used in your project.

| Finger | Key |
|---------|-----|
| Thumb | Q |
| Index | W |
| Middle | E |
| Ring | R |
| Pinky | T |

---

## How to Play

1. Launch the game.
2. Read the object displayed on the screen.
3. Form the correct grip using the finger keys.
4. Pick up the requested object.
5. Move it into the assessment zone.
6. Keep it inside the zone until the timer reaches zero.
7. Repeat until all objects are collected.

Submitting the wrong object immediately ends the game.

---

## Project Structure

```
Assets
├── Scripts
│   ├── Hand
│   ├── Interaction
│   ├── Objects
│   ├── Game
│   └── UI
├── Prefabs
├── Materials
├── Models
├── Scenes
└── ScriptableObjects
```

---

## Built With

- Unity 6
- C#
- Unity Input System
- TextMesh Pro

---

## Assignment Objective

The objective of this project was to design gameplay centered around five dedicated input keys where every key has a meaningful role.

Grip Says achieves this by mapping one key to each finger and requiring different finger combinations for different objects, ensuring all five keys are actively used throughout gameplay.

---

## Future Improvements

- More object types
- Additional grip gestures
- Dynamic grip points
- Levels and increasing difficulty
- Audio and visual polish
- Leaderboard
- Accessibility options

---

## Credits

Developed by **Aditya Gupta**

Created as part of a Game Development Internship assignment exploring innovative five-key interaction mechanics.
