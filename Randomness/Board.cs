using System;

namespace Randomness {
    /// <summary>
    ///     The board object, consists of rings. Should best derive from IEnumerable.
    /// </summary>
    class Board {
        #region Fields
        /// <summary>
        ///     Starting angle of each ring
        /// </summary>
        private static readonly float[] StartAngles = {0f, 16.67f, 4f, 40.4f};

        /// <summary>
        ///     The value of each node (excite tile or not). NodeSetup[i] represents a ring, NodeSetup[i][j] represents a node.
        /// </summary>
        private static readonly bool[][] NodeSetup = {
            new[] {false, true},
            new[] {false, false, true, false, false, false, true, false},
            new[] {
                false,
                true,
                false,
                false,
                true,
                false,
                false,
                false,
                true,
                false,
                false,
                false,
                true,
                false,
                false,
                true,
                false,
                false
            },
            new[] {false, false, false, false, false, false, false, true}
        };
        #endregion

        #region Properties
        /// <summary>
        ///     The list of rings this board has
        /// </summary>
        public Ring[] Rings { get; set; } = new Ring[NodeSetup.Length];

        /// <summary>
        ///     Amount of rings this board has
        /// </summary>
        public int RingCount => Rings.Length;

        /// <summary>
        ///     The starting node of the board
        /// </summary>
        public Node StartNode => this[0][1];
        #endregion

        #region Constructor
        /// <summary>
        ///     Default constructor
        /// </summary>
        public Board() {
            // Fill up rings with objects
            for (int i = 0; i < NodeSetup.Length; i++) {
                Rings[i] = new Ring(this, NodeSetup[i], StartAngles[i]);
            }
        }
        #endregion

        #region Methods
        public RingInfo FindRing(Ring ring) {
            return FindRing(ring.ID);
        }

        public RingInfo FindRing(int ID) {
            var result = new RingInfo {Board = this};
            var done = false;
            for (int i = 0; i < RingCount; i++) {
                if (Rings[i].ID == ID) {
                    result.RingIndex = i;
                    done = true;
                    break;
                }
            }
            if (!done)
                throw new ArgumentOutOfRangeException(nameof(ID));
            return result;
        }

        /// <summary>
        ///     Find a node and returns its info
        /// </summary>
        /// <remarks>Should be moved to the class <see cref="Ring"/></remarks>
        /// <param name="node"></param>
        /// <returns></returns>
        public NodeInfo FindNode(Node node) {
            return FindNode(node.ID);
        }

        /// <summary>
        ///     Finds a node and returns its info based on its ID
        /// </summary>
        /// <remarks>Should be moved to the class <see cref="Ring"/></remarks>
        /// <param name="ID"></param>
        /// <returns></returns>
        public NodeInfo FindNode(int ID) {
            var result = new NodeInfo {Board = this};
            var done = false;
            for (int i = 0; i < RingCount; i++) {
                for (int j = 0; j < Rings[i].NodeCount; j++) {
                    if (Rings[i][j].ID == ID) {
                        result.RingIndex = i;
                        result.NodeIndex = j;
                        done = true;
                        break;
                    }
                    if (done)
                        break;
                }
            }
            if (!done)
                throw new ArgumentOutOfRangeException(nameof(ID));
            return result;
        }

        /// <summary>
        ///     Print the board
        /// </summary>
        public void Print() {
            Console.WriteLine("Rings: " + Rings.Length);
            for (int i = 0; i < Rings.Length; i++) {
                var ring = Rings[i];
                Console.Write("Ring " + i + " nodes: " + ring.Nodes.Length + "\n  ");
                foreach (Node node in ring.Nodes) {
                    Console.Write(node.IsExcite ? "E" : "n");
                }
                Console.WriteLine();
            }
        }
        #endregion

        #region Operators
        /// <summary>
        ///     The []-operator to access the rings
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Ring this[int i] => Rings[i];
        #endregion
    }

    /// <summary>
    ///     A single ring from which the <see cref="Board"/> consists, each containing multiple <see cref="Node"/>s. Should
    ///     best derive from IEnumerable.
    /// </summary>
    class Ring {
        #region Fields
        /// <summary>
        ///     Counter to assign an ID to this ring
        /// </summary>
        private static int IdCounter;
        #endregion

        #region Properties
        /// <summary>
        ///     This ring unique ID
        /// </summary>
        public int ID { get; }

        /// <summary>
        ///     Reference to its parent
        /// </summary>
        public Board Board { get; }

        /// <summary>
        ///     List of all the nodes on this ring
        /// </summary>
        public Node[] Nodes { get; set; }

        /// <summary>
        ///     Number of nodes on this ring
        /// </summary>
        public int NodeCount => Nodes.Length;

        /// <summary>
        ///     At what angle the first node lies
        /// </summary>
        public float StartAngle { get; }

        /// <summary>
        ///     The index of this ring according to its parent
        /// </summary>
        public int Index => Board.FindRing(this).RingIndex;
        #endregion

        #region Constructor
        /// <summary>
        ///     Constructor which sets its parent <paramref name="board"/>, the nodes it contains with <paramref name="nodes"/> and
        ///     the starting angle <paramref name="startAngle"/>.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="nodes"></param>
        /// <param name="startAngle"></param>
        public Ring(Board board, bool[] nodes, float startAngle) {
            ID = IdCounter++;
            Board = board;
            //_idx = nodes.NodeCount;
            StartAngle = startAngle;
            Nodes = new Node[nodes.Length];
            for (int i = 0; i < nodes.Length; i++)
                Nodes[i] = new Node(this, nodes[i]);
        }
        #endregion

        #region Operators
        /// <summary>
        ///     The []-operator to easily access nodes
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Node this[int i] => Nodes[i % Nodes.Length];

        /// <summary>
        ///     Go <paramref name="i"/> amount of rings up. Negative number is down.
        /// </summary>
        /// <remarks>Does not check for out of bounds.</remarks>
        /// <param name="ring"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static Ring operator +(Ring ring, int i) {
            return ring.Board[ring.Index + i];
        }
        #endregion
    }

    class Node {
        #region Fields
        /// <summary>
        ///     Counter to assign an ID to this node
        /// </summary>
        private static int IdCounter;
        #endregion

        #region Properties
        /// <summary>
        ///     Its parent
        /// </summary>
        public Ring Ring { get; }

        /// <summary>
        ///     The unique ID associated with this node
        /// </summary>
        public int ID { get; }

        /// <summary>
        ///     Is the node an excite node?
        /// </summary>
        public bool IsExcite { get; set; }

        /// <summary>
        ///     The index of this node in its ring
        /// </summary>
        public int Index => Ring.Board.FindNode(this).NodeIndex;

        /// <summary>
        ///     The angle at which this node resides
        /// </summary>
        public float Angle {
            get {
                var anglePerNode = 360f / Ring.Nodes.Length;
                return Ring.StartAngle + Index * anglePerNode;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        ///     Constructor which sets its parent as <paramref name="ring"/> and its value <paramref name="excite"/>
        /// </summary>
        /// <param name="ring"></param>
        /// <param name="excite"></param>
        public Node(Ring ring, bool excite) {
            ID = IdCounter++;
            Ring = ring;
            IsExcite = excite;
        }
        #endregion

        #region Operator
        /// <summary>
        ///     Go up or down a couple of nodes
        /// </summary>
        /// <param name="node"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static Node operator +(Node node, int val) {
            return node.Ring[node.Index + val];
        }
        #endregion
    }

    /// <summary>
    ///     Holds information about a ring
    /// </summary>
    struct RingInfo {
        /// <summary>
        ///     The parent of this ring
        /// </summary>
        public Board Board;

        /// <summary>
        ///     Its index according to its parent
        /// </summary>
        public int RingIndex;

        /// <summary>
        ///     The ring itself
        /// </summary>
        public Ring Ring => Board[RingIndex];

        /// <summary>
        ///     String representation of this ring
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return $"R{RingIndex}";
        }
    }

    /// <summary>
    ///     Information about a node, i.e. in what board and ring it resides and the index number of it
    /// </summary>
    struct NodeInfo {
        /// <summary>
        ///     The board the node is in
        /// </summary>
        public Board Board;

        /// <summary>
        ///     The index of the ring the node is in according to the board
        /// </summary>
        public int RingIndex;

        /// <summary>
        ///     The index of this node according to its parent
        /// </summary>
        public int NodeIndex;

        /// <summary>
        ///     The ring the node resides in
        /// </summary>
        public Ring Ring => Board[RingIndex];

        /// <summary>
        ///     The node itself
        /// </summary>
        public Node Node => Board[RingIndex][NodeIndex];

        /// <summary>
        ///     Its string representation
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return $"R{RingIndex}N{NodeIndex}";
        }
    }
}
