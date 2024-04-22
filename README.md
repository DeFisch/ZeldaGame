# ZeldaGame
OSU CSE 3902 Zelda Game Project

## Team member:
- Leslie Cobbold	cobbold.4@osu.edu
- Olu Fasoro	    fasoro.2@osu.edu
- Olivia Maynard	maynard.341@osu.edu
- Dan Perry	      perry.2440@osu.edu
- Daniel Feng     feng.1270@osu.edu
- Zhiyang Xu      xu.4082@osu.edu

## Sprint 4 Task Distribution:
- Enemies, audio: Daniel
- Item and in-game HUD: Olivia
- Item/bug fixes: Olu
- Player/Weapon: Dan
- NPC: Leslie
- Separate screen HUD, item swapping: Zhiyang

## Key and Mouse Mappings:
- Player Controls
  - "wasd" or arrow keys for movements
  - "z" or "n" for sword attack
  - "b" to use an inventory item (arrows, boomerang, bomb, candle/flames)
  - Walk through doors using arrow keys to walk through the corresponding rooms
- Other
  - "q" to quit, "r" to reset the game
  - "h" to go to the inventory
  - "a/d" to select inventory items
  - "<-", "->" arrow keys to select room
  - Enter to teleport to the selected room
  - Spacebar to start the game and return to the game from the inventory
  - "p" to pause/unpause
  - "m" to mute/unmute
- Controls for Testing
  - Click on doors to move through the corresponding rooms

## Items:
- Bomb
  - The player starts with eight bombs, and each time a bomb is thrown one bomb is used up. When a bomb is picked up after an enemy drop, four bombs are placed in Link's inventory. Link can carry no more than eight bombs at a time.
- Map
  - When the Map is picked up, the HUD displays the total layout of the dungeon in green in the upper left corner.
- Compass
  - When the Compass is picked up, the HUD displays the location of the final boss in red, in the map area in the upper left.
- Blue Rubies and Yellow Rubies
  - Blue rubies increment Link's total number of rubies by 5, and yellow (flashing yellow/blue) rubies increment Link's total number of rubies by 1.
- Clock
  - If an enemy drops a Clock on a screen after being killed, when Link picks it up, the enemies on the screen will stop moving. This effect only lasts while Link is on that particular screen, so when he leaves the effect in that room ends.
- Heart
  - Picking up a Heart allows Link to restore one of his life hearts, shown in the upper right corner of the HUD.
- Heart Container
  - Picking up a Heart Container gives Link one extra spot to store life hearts, increasing his maximum health.
- Triforce
  - The Triforce is located in the last room of the dungeon, past the final boss. Picking up the Triforce triggers the Game Win screen.
- Life Potion
  - The Life Potion can only be purchased, for 5 rubies, from a particular NPC in the NPC room. If Link doesn't have at least five rubies, he'll simply walk over the item and not be able to purchase it. The Life Potion restores all of Link's lives upon purchase.

## Player Weapons Mannual
- Orange Arrow: cost 1 ruby, deals 2 damage
- Blue Arrow: cost 3 ruby, deals 3 damage
- Orange/Blue Boomerang: 0 cost, fly through wall, only weapon that can kill Keese (flying bats)
- Bomb: 2 damage, can damage/kill player
- Flame: 0 damage, purely cosmetic (has knockback) 

## Curse of Darkness Game Mode
![curse of darkness visual](https://github.com/DeFisch/ZeldaGame/blob/main/README_img/darkness.png)

## Updates:
- Arrow and Sword Mechanics Updates
  - Using normal arrows costs one ruby each when used. Magical arrows cost three rubies each when used.
  - When Link has full health, the sword is able to shoot out as an exploding projectile!
- Inventory
  - To change the item in Link's offhand, go to the inventory by pressing "h". Use the mouse keys to pick Link's offhand item, press space to exit the inventory, and then use "b" to use that item. 

