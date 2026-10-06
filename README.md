# Bludoku

A short overview of the gameplay systems implemented in this Unity project.

## Implemented features

- Combos increase by the number of cleared rows, columns, and 3×3 squares.
- A combo remains active through two moves without a clear. The heart indicates active, warning, and critical states; the multiplier appears at `x2` and above.
- Points earned from clears are multiplied by the active combo and the existing booster.
- Cleared groups fly toward the multiplier, which animates on arrival; a light camera shake accompanies each arrival. The effect uses a dedicated prefab with a transparent sprite shader.
- Analytics events cover game starts, piece moves, booster activation, second chances, and game over. Events are currently written to the console.

## Scenes

Main game scenes: `Assets/_Bludoku/Scenes/MainMenu.unity` and `Assets/_Bludoku/Scenes/GameScene 1.unity`.
