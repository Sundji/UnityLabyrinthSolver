# UnityLabyrinthSolver

- the solver moves tile by tile in one direction until an exit or a wall are reached
- when a wall is reached, the solver changes directionž
- when there are no other available directions, the solver goes back tile by tile until a tile, from which another direction can be chosen, is reached
- labyrinths are generated from files included in the project
- a simple user interface allows the user to choose a labyrinth, create it and start the solver
- the more complex the labyrinth is, the more time it takes to find a solution and the memory usage rises (depending on the labyrinth size, structure and the chosen starting point, Unity could potentially crash due to high memory usage)
- messages about finding the exit or not finding a solution written as debug log errors
- five labyrinth files included
- made for the resolution 1920x1080
- made with Unity 2021.3.3f1

![alt_text](https://github.com/Sundji/UnityLabyrinthSolver/blob/main/Assets/Screenshots/Labyrinth1.png?raw=TRUE)
![alt_text](https://github.com/Sundji/UnityLabyrinthSolver/blob/main/Assets/Screenshots/Labyrinth2.png?raw=TRUE)
![alt_text](https://github.com/Sundji/UnityLabyrinthSolver/blob/main/Assets/Screenshots/Labyrinth3.png?raw=TRUE)
![alt_text](https://github.com/Sundji/UnityLabyrinthSolver/blob/main/Assets/Screenshots/Labyrinth4.png?raw=TRUE)
![alt_text](https://github.com/Sundji/UnityLabyrinthSolver/blob/main/Assets/Screenshots/Labyrinth5.png?raw=TRUE)
