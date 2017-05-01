using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Randomness {
    class Program {
        /// <summary>
        ///     The board to play on
        /// </summary>
        private static Board board = new Board();

        /// <summary>
        ///     All the players in the game
        /// </summary>
        private static List<Player> players = new List<Player>();

        /// <summary>
        ///     Number of players in the game
        /// </summary>
        private static int _numPlayers = 4;

        /// <summary>
        ///     Dice faces
        /// </summary>
        private static readonly Dice _dice = new Dice(new[] {1, 2, 3, 4, 5, 6});

        /// <summary>
        ///     Number of games to play
        /// </summary>
        private static int _gamesToPlay = 1;

        /// <summary>
        ///     Resets the position of the players to start a new game
        /// </summary>
        static void Reset() {
            players.Clear();
            for (int i = 0; i < _numPlayers; i++)
                players.Add(new Player(board, "P" + i));
        }

        /// <summary>
        ///     Entry point of the application
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) {
            //Check arguments
            if (args.Length == 2) {
                if (!int.TryParse(args[0], out _numPlayers) || _numPlayers <= 0) {
                    Console
                        .WriteLine("First argument should be an integer (>0) value representing the amount of players");
                    return;
                }
                if (!int.TryParse(args[1], out _gamesToPlay) || _gamesToPlay <= 0) {
                    Console
                        .WriteLine("Second argument should be an integer (>0) value representing the amount of games that should be played");
                    return;
                }
            }

            //If more games to play, do not print full game
            if (_gamesToPlay > 1) {
                Console.SetOut(TextWriter.Null);
                Console.SetError(TextWriter.Null);
            }

            // Start playing some games
            int qCount = 0; //question count
            for (int i = 0; i < _gamesToPlay; i++) {
                // Play a game
                Reset();
                int playingPlayer = 0;
                int turnCount = 0;
                Console.WriteLine("TURN " + turnCount++);
                while (!players.Any(p => p.Exited)) {
                    qCount++;
                    var currPlayer = players[playingPlayer];
                    Console.Write(currPlayer.Name + ": ");
                    var diceRoll = _dice.Roll();
                    Console.Write(diceRoll + " -> ");
                    currPlayer.Move(diceRoll);
                    var questionType = currPlayer.Node.IsExcite ? "E" : "n";
                    var questionResult = currPlayer.Answer() ? "Y" : "N";
                    Console.WriteLine(questionType + "/" + questionResult);

                    playingPlayer = (playingPlayer + 1) % _numPlayers;
                    if (playingPlayer == 0) {
                        Console.WriteLine("Resume: " +
                                          players.Select(p => p.ToString()).Aggregate((a, b) => a + "|" + b));
                        Console.WriteLine();
                        Console.WriteLine("TURN " + turnCount++);
                    }
                }
                var winner = players.OrderByDescending(p => p.Points).First();
                Console.WriteLine("\n" + winner.Name + " won with " + winner.Points + " points!");
                Console.WriteLine("Resume: " + players.Select(p => p.ToString()).Aggregate((a, b) => a + "|" + b));
                Console.WriteLine();
                Console.WriteLine("-------------------------------------------------------------------------");
                Console.WriteLine();
            }

            // Gametime is over
            if (_gamesToPlay > 1) {
                var stdout = new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = true};
                Console.SetOut(stdout);
            }

            // Show count
            Console.WriteLine($"Average question count: {(float)qCount / _gamesToPlay + "\n"}");
        }
    }
}
