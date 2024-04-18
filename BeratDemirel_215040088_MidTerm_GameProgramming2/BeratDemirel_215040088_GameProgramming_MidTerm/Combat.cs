using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeratDemirel_215040088_GameProgramming_MidTerm
{
    public class Combatant : Entity
    {
        // Property to keep track of the number of times the combatant missed an attack
        public int MissCount { get; private set; } = 0;

        // Constructor to initialize the Combatant with name, health, level, and armor
        public Combatant(string name, int health, int level, int armor)
            : base(name, health, level, armor)
        {
        }

        // Method to increment the MissCount when the combatant misses an attack
        public void IncrementMissCount()
        {
            MissCount++;
        }
    }
}