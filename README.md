**SpaceExplorer**

### Objective
The goal of **SpaceExplorer** is for players to control a spaceship, navigate through asteroid fields, and collect stars to score points while avoiding obstacles and enemies.

### Game Concept
In **SpaceExplorer**, the player controls a spaceship in a dynamic space environment. The game features multiple levels, each presenting unique challenges such as asteroid fields, enemy ships, and star collection. The player must survive as long as possible, collect stars to earn points, and defeat enemies to progress further.

### Game Elements
#### 1. Spaceship (Player Object)
**Description:**
The player controls a 2D spaceship that can move and shoot in a space environment.

**Functionality:**
- **Movement:** Use arrow keys or WASD to navigate in all directions.
- **Shooting:** Press the spacebar or left mouse button to fire lasers at enemies and asteroids.
- **Lives:** The spaceship starts with 3 lives, which decrease when hit by enemy lasers or upon colliding with asteroids.
- **Score:** Collect stars to increase your score.

#### 2. Asteroids
**Description:**
2D asteroids float randomly in space, creating obstacles for the player.

**Functionality:**
- **Movement:** Asteroids move randomly at varying speeds.
- **Collision:** If the player's spaceship collides with an asteroid, they lose one life.
- **Destruction:** Asteroids can be destroyed by player lasers, earning 10 points per asteroid.

#### 3. Blue Stars
**Description:**
2D stars scattered throughout the space environment for players to collect.

**Functionality:**
- **Collection:** Increases the player's attack speed by 3% upon collection.
- **Spawn:** Stars spawn randomly at regular intervals.

#### 4. Red Stars
**Description:**
2D stars scattered throughout the space environment for players to collect.

**Functionality:**
- **Collection:** Restores one life upon collection.
- **Spawn:** Stars spawn randomly at regular intervals.

### Game Mechanics
#### Scoring System
- Asteroids Destroyed: +10 points per asteroid.

#### Life System
- The player starts with 3 lives.
- Colliding with asteroids or enemy lasers reduces lives.
- Lives can be restored by collecting **Red Stars**.

#### Controls
- **Movement:** Arrow keys or WASD.
- **Shooting:** Spacebar or left mouse button.
- **Pause:** Press **Esc** to pause and access the menu.

### Development Notes
This game was developed using **Unity** to reinforce the following concepts:
- Unity interface and navigation
- GameObject management and manipulation
- Unity's component system
- Scene creation and management
- Physics and colliders
- TextMeshPro

### How to Play
1. Download or clone the repository.
2. Open **SpaceExplorer.exe** to play.

