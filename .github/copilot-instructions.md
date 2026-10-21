# Combat Power Tweaker GitHub Copilot Instructions

## Mod Overview and Purpose
**Combat Power Tweaker** is a mod designed for RimWorld players who want more control over the game's raid balance mechanism. By specifically allowing the modification of the combat power value of all pawn kinds, this mod enables adjustments to raid composition and difficulty. Combat power determines the "cost" of a pawn during raids, with higher power leading to fewer but stronger raiders, and lower power producing larger raiding parties of weaker units. This feature allows players to tailor their experiences by modifying individual pawn values to suit their playstyle, whether they seek a more manageable defense scenario or an extreme challenge by drastically altering values.

## Key Features and Systems
- **Adjustable Combat Power Values**: Modify the combat power of individual pawns, especially notable enemies such as Centipedes, to customize raid difficulty.
- **Settings Management**: Through the settings interface, players can alter values and immediately impact gameplay.
- **User-Friendly Interface**: Simple methods to adjust values, aided by interface tools for easy navigation and selection.
- **Seamless Integration**: Updates and changes are applied without disrupting gameplay or requiring deep technical changes from the user.

## Coding Patterns and Conventions
- **C# Structure**: The mod is structured in three primary C# files, distinguishing various aspects of functionality:
  - `CombatPowerTweakerMod.cs` handles initialization and settings UI.
  - `CombatPowerTweaker.cs` contains core functionality for modifying values.
  - `CombatPowerTweakerSettings.cs` manages player-specific settings and data exposure.
- **Naming Conventions**: PascalCase is employed for class and method names, ensuring clarity and maintainability.

## XML Integration
- **Overview**: Currently, there are no XML files in this mod; all adjustments and settings are managed through C#.
- **Potential Additions**: For future expansions, XML can be integrated, especially for game element definitions or translations.

## Harmony Patching
- **Purpose**: Leverage Harmony for non-destructive patching to modify base game behavior without directly altering the original code.
- **Application**: Ensure mods are compatible with different game versions and other mods by following the Harmony patching methodology.
- **Example**: Patch methods of the game’s raid calculation to read custom combat power values set by the mod.

## Suggestions for Copilot
- **Writing XML Definitions**: If future updates require XML integration, employ Copilot to generate templates or data structure examples for ease of integration.
- **Harmony Patch Generation**: Use Copilot to assist with creating and refining Harmony patches to ensure stable and efficient functionality.
- **Code Refactoring**: Leverage Copilot to offer code refactoring suggestions to maintain robustness and performance as the mod evolves.
- **Testing Enhancements**: Use Copilot to generate unit tests to validate functionality, ensuring the tweaking adjustments are applied correctly.

Utilizing these Copilot-assisted areas can significantly enhance development efficiency and maintain high code quality as the Combat Power Tweaker mod continues to evolve. Happy modding!

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

