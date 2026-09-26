# Card Tree Puzzle

A deckbuilding game played on a randomly generated tree. Your cards tell you things about the branches ahead, like how many monsters are close by or whether the exit is near, and you use that to find your way out.

- Play: No public build
- Made: January to February 2025, solo prototype
- Team: [@mfclinton](https://github.com/mfclinton)
- Engine: Unity, C#

This is an export of a private repo with only the code I wrote. Art, the Unity project files, and third party plugins aren't included. The history is squashed into one commit.

## What I built

- I kept the game logic separate from Unity. The rules, card effects, deck, and turn state are plain C# in `Scripts/Core/`, which is its own assembly with engine references turned off. `Scripts/Unity/` only deals with visuals and input.
- Tree generation with settings for depth, branching, and how often each kind of node shows up.
- Card effects that count nodes or compare subtrees. Each card is a data asset, and `[SerializeReference]` lets one card stack several effect types in the inspector.
- A command processor for player actions and a typed event bus, with handler priorities, that the visuals listen to.
- The hand fans your cards along a spline and animates them with DOTween, lifting the one you hover or select.
- NUnit tests for deck building, card effects, game state, and tree generation.
