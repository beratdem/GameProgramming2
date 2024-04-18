using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeratDemirel_215040088_GameProgramming_MidTerm
{
    public class Entity
    {
        // Property to store the name of the entity
        public string Name { get; set; }

        // Property to store the health points of the entity
        public int Health { get; set; }

        // Property to store the level of the entity
        public int Level { get; set; }

        // Property to store the armor points of the entity
        public int Armor { get; set; }

        // Constructor to initialize the Entity with name, health, level, and armor
        public Entity(string name, int health, int level, int armor)
        {
            Name = name;
            Health = health;
            Level = level;
            Armor = armor;
        }
    }
}

