# Excite!
Simulates a board game to know the average amount of turns that should be played. This was part of a project for teaching classes where we 'invented' a board game to play with the class.

## Running the program
Go to the [release page](https://github.com/didii/Randomness/releases), download the binaries and unzip it. Run the game from the command line to see the output.

### Command line arguments
    Excite.exe [NumPlayers] [NumSimulations]
        NumPlayers        (Optional, Integer, Default = 4) How many players take part in the game.
        NumSimulations    (Optional, Integer, Default = 1) How many games that should be simulated. If this
                          is equal to 1, a detailed report of the game is printed. For any other valid number
                          only the average number of questions that were asked is printed.

### Understanding the program
There are a lot of abbreviations used for the simulation. Here is preview of a single turn:

    TURN 4
    P0: 5 -> n/N
    P1: 4 -> n/N
    P2: 2 -> E/Y
    P3: 2 -> n/Y
    P4: 1 -> E/N
    Resume: 4R1N0|5R1N0|5R1N1|6R1N7|5R2N16

The first line is simply the turn count that is mentioned before every turn. Then `P0:`, `P1:`, etc. are the names of the players. After the semicolon `:` the dice roll is mentioned. After the arrow `->` the first character is either an `n` if the player landed on a normal node or an `E` for an excite node. A slash `/` is then used whereafter you see whether or not the player answered the question correctly or not (`Y` for correct, `N` for incorrect).

The last line tells where everyone now is and how many points they have. It has the following format: `#R#N#`. The first number is the score of the player, then the ring index, then the node index on the ring.

## How the game works
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
