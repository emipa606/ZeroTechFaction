# GitHub Copilot Instructions for ZeroTech-Faction (Continued) Mod

## Mod Overview and Purpose

ZeroTech-Faction (Continued) is an updated version of LingLuo's original RimWorld mod, aiming to refine and expand the gameplay experience related to the Zero Tech faction. This mod introduces the "Moonlight Blessed," a technologically advanced faction that emphasizes innovation over chaos and violence. The mod enhances interactions with other factions, particularly making the Zero Tech faction not always hostile toward the Empire, and offers a fresh narrative direction focused on technological advancements and societal order.

## Key Features and Systems

- **New Faction: Moonlight Blessed**  
  A technologically inclined faction that values innovation, offering a distinct narrative contrast to other factions in RimWorld.

- **Revised Faction Relations**  
  The Zero Tech faction is no longer a permanent enemy of the Empire, which allows for nuanced diplomatic interactions.

- **Specialized Apparel and Equipment**  
  Classes like `LingMoonDuster`, `LingMoonShield`, and others provide unique clothing and protective gear that enhance gameplay through their specific functions and bonuses.

- **Unique Weapons and Skills**  
  Classes such as `TacticsBullet`, `Verb_LaunchIncidentMoon`, and `Verb_MeleeAttackDamageAndJump` introduce novel combat mechanics and skills.

## Coding Patterns and Conventions

- **Namespaces and Class Structure**: Organized per common practice in RimWorld modding. Each core functional component is encapsulated in its respective class, such as `Gizmo_LingMoon_ShieldStatus` and `LingMoonRoarComp`.

- **Method Naming**: Methods are named using PascalCase, reflecting standard C# conventions. Method names are indicative of their functionality, such as `AbsorbedDamage` and `Break` in `LingMoonShield`.

- **Access Modifiers**: Internal and public access modifiers are appropriately used to encapsulate class functionality and maintain code clarity and integrity.

## XML Integration

- The mod may include XML files to define faction characteristics, apparel, weapons, and more for integration into RimWorld. XML tags should align with those recognized by RimWorld, ensuring correct parsing and implementation of asset attributes and definitions.

- Utilize the XML `<Defs>` framework to define new factions, apparel, and weaponry. Ensure that identifiers are consistent across XML and C# code for seamless interaction.

## Harmony Patching

- Harmony is utilized to patch existing game mechanics, allowing for modifications without directly altering vanilla game code. 
- Implement Harmony patches for aspects like faction relations and unique attributes of the Moonlight Blessed faction.
- Create postfix and prefix patches where necessary to extend or alter method behaviors cleanly.

## Suggestions for Copilot

- **Understanding the Context**: Copilot can suggest code snippets that align with the core theme of technological advancement, reflecting the narrative of the Moonlight Blessed faction.
- **Consistent Naming Suggestions**: Ensure that Copilot suggestions use descriptive and consistent naming conventions across classes and methods to maintain readability.
- **XML Definitions**: Suggest XML schema for defining factions, apparel, and abilities integrated from the C# codebase.
- **Harmony Instructions**: Propose Harmony patches to non-invasively enhance or override existing game systems.

This documentation aims to guide contributors in maintaining coding convention uniformity, integrating new features smoothly, and leveraging GitHub Copilot effectively in the development of ZeroTech-Faction (Continued).
