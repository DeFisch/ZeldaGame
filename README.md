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
  - "1,2,3,4,5,6" for different weapons (1,2 for arrows, 3,4 for boomerang, 5 for bombs, 6 for flames)
  - Walk through doors using arrow keys to walk through the corresponding rooms
- Other
  - "q" to quit, "r" to reset the game
  - "h" to go to the inventory
  - Spacebar to start the game and return to the game from the inventory
  - "p" to pause/unpause
  - "m" to mute/unmute
- Mouse
  - Click on doors to move through the corresponding rooms

## Updates and Items:
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
  - Arrow and Sword Mechanics
    - Using normal arrows costs one ruby each when used. Magical arrows cost three rubies each when used.
    - When Link has full health, the sword is able to shoot out as an exploding projectile! 

## GitHub basic commands:
a. Download the repository to your local machine
```shell
git clone https://github.com/DeFisch/ZeldaGame.git
```
b. Update the remote changes of the repository to your local machine
```shell
git pull
```
c. Check the changes you made since last commit/pull
```shell
git status
```
d. Record your changes of the file
```shell
git add [filename]
```
or alternatively, record all the changes you made to the repository
```shell
git add *
```
e. Gather all your record of changes to a single commit, commit message usually explains what functionality you adds in these changes
```shell
git commit -m "[your commit message here]"
```
g. Update all your local commits to the GitHub repo so everyone can access it
```shell
git push
```
