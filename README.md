# Excite!
Simulates a board game to know the average amount of turns that should be played. This was part of a project for teaching classes where we 'invented' a board game to play with the class.

## How to play
The board is an atom (Kr). You have the core and electrons around it ordered on shells. The electrons are regarded as nodes for players to walk on. The goal is to obtain the most points by answering questions and the game ends when a player escapes from the atom.

### Setup
1. Every player places their pawn on the core of the atom.
2. The question cards are placed onto their corresponding place on the board (normal and excite questions)
3. The first player may take the dice and is further referred to as the active player.

### Taking a turn
1. The active player rolls the dice and moves that amount of nodes forward (clock-wise). If he were at the core, the first move is to go to the node directly above the core.
2. The player at the right of the active player takes a card corresponding on the node the active player ended on (normal or excite) and reads the question aloud.
3. The active player answers the question and gains points according to the table below. If an excite question was answered correctly, the active player *excites* and moves its pawn to the next shell. Move your pawn perpendicular of your current node to the next shell and follow the ring clockwise until you are again at a node.

    | | Normal | Excite |
    |-|:-:|:-:|
    Correct answer | 1 | 2
    Wrong answer | 0 | 0

4. The player left to the active player is now the new active player.

### End of the game
The game immediately ends if any player excites from the outer shell. Points are compared and the player with the highest score wins the game.

## The play board
This is the atom Krypton. White nodes are normal nodes while blue ones are excite nodes.

![Board](http://i.imgur.com/gVpSJ4D.png "Sample of the board")
