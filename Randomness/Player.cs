using System;
using System.Collections.Generic;

namespace Randomness {

    /// <summary>
    /// Represents a pawn moving on the board
    /// </summary>
    class Player {
        #region Fields
        /// <summary>
        /// Holds the chance that the player will correctly answer a certain question
        /// </summary>
        private readonly Dictionary<bool, float> _succesChance = new Dictionary<bool, float> {{true, .8f}, {false, .7f}};
        #endregion

        #region Properties
        /// <summary>
        /// The node this player is on
        /// </summary>
        public Node Node { get; private set; }

        /// <summary>
        /// The ring where the player is in
        /// </summary>
        public Ring Ring => Node.Ring;

        /// <summary>
        /// The board the player is on
        /// </summary>
        public Board Board => Ring.Board;

        /// <summary>
        /// Number of points the player has
        /// </summary>
        public int Points { get; private set; }

        /// <summary>
        /// If the player has exited the board (ends the game)
        /// </summary>
        public bool Exited { get; private set; } = false;

        /// <summary>
        /// The name of this player
        /// </summary>
        public string Name { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Puts the player on the board and gives him a name
        /// </summary>
        /// <param name="board"></param>
        /// <param name="name"></param>
        public Player(Board board, string name) {
            Node = board.StartNode;
            Name = name;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Moves the player <paramref name="steps"/> nodes
        /// </summary>
        /// <param name="steps"></param>
        public void Move(int steps) {
            Node += steps;
        }

        /// <summary>
        /// Ask the player to answer a question. Auto-excites if correct
        /// </summary>
        /// <returns>Whether or not he anwered correctly</returns>
        public bool Answer() {
            // Answer the question
            var correct = Rand.NextFloat() < _succesChance[Node.IsExcite];
            if (!correct)
                return false;
            if (Node.IsExcite) {
                // Excite question
                Points += 2;
                Excite();
            }
            else // Normal question
                Points += 1;
            return true;
        }

        /// <summary>
        /// Go one ring up!
        /// </summary>
        public void Excite() {
            Ring nextRing;
            try {
                nextRing = Ring + 1;
            }
            catch (IndexOutOfRangeException) {
                Exited = true;
                Points += 2;
                return;
            }
            var yep = false;
            for (int i = 0; i < nextRing.NodeCount; i++) {
                if (nextRing[i].Angle < Node.Angle && Node.Angle < nextRing[i + 1].Angle) {
                    Node = nextRing[i + 1];
                    yep = true;
                    break;
                }
            }
            if (!yep)
                Node = nextRing[0];
        }

        /// <summary>
        /// String representation of this player
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return Points + Board.FindNode(Node).ToString();
        }
        #endregion
    }
}
