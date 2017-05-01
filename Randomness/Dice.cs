namespace Randomness {

    /// <summary>
    /// Dice class. Simply rolls a dice
    /// </summary>
    class Dice {
        /// <summary>
        /// List of possible outcomes
        /// </summary>
        private readonly int[] _outcomes;

        /// <summary>
        /// Constructor telling which kind of faces it has
        /// </summary>
        /// <param name="outcomes"></param>
        public Dice(int[] outcomes) {
            _outcomes = outcomes;
        }

        /// <summary>
        /// Rolls the dice
        /// </summary>
        /// <returns></returns>
        public int Roll() {
            int idx = Rand.Next(_outcomes.Length);
            return _outcomes[idx];
        }
    }
}
