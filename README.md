# Bludoku

A short overview of the gameplay systems implemented in this Unity project. Time about 12 hours.

## Implemented features

- The board is a 9×9 grid. Players place pieces to complete rows, columns, or eligible 3×3 squares; cleared blocks are removed.
- Combos increase by the number of cleared groups. After two unsuccessful moves, the heart shows its critical state; a third unsuccessful move breaks the combo.
- The heart communicates combo state with color and pulsing. The multiplier label appears at `x2` and above.
- Points earned from clears are multiplied by the active combo and the existing booster.
- Cleared groups fly toward the multiplier. The multiplier grows on each arrival, accompanied by a light camera shake. The flying shape uses a dedicated prefab with a transparent sprite shader.
- Particles spawn at cleared block positions, scatter sideways, fall with pseudo-gravity, and shrink as they disappear.
- When an active combo breaks, the heart separates into as many semi-transparent white texture slices as the combo count. The pieces move away from the center, rotate slightly, and fall under gravity.
- Analytics events cover game starts, piece moves, booster activation, second chances, and game over. Events are currently written to the console.

## Configuration

- Combo rules: `Assets/_Bludoku/Scripts/Score/ComboConstants.cs`.
- Particle, combo UI, flight, camera shake, and heart shatter settings: `Assets/_Bludoku/Scripts/Effects/EffectsConstants.cs`.

## Anything you would improve or extend with more development time
1. Game really lacks SFX, so next tep is to add sound feedback. Simple short quiet sound will do the job.
2. Some powrups, such as figures morfing, board shuffle(i.e. rearrange 3*3 blocks randomly, or shift all blocks to the side, up or down), some line/rows/box destruction without continuing combo or scores, forced figure placement...
