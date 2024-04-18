using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static BeratDemirel_215040088_GameProgramming_MidTerm.Enums;

namespace BeratDemirel_215040088_GameProgramming_MidTerm
{
    public class Battle
    {
        // Creating a Random object for generating random numbers
        private readonly Random random = new();

        // Method to start the battle
        public async Task StartBattleAsync(Combatant player, Combatant enemy)
        {
            var name = "";

            // Prompting the user to enter their name
            while (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Please enter your name:");
                name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Name cannot be empty or whitespace. Please try again.");
                }
            }

            // Setting the player's name
            player.Name = name;
            Console.WriteLine($"Welcome, {player.Name}!");

            // Displaying introductory messages
            Console.WriteLine("Get ready to witness the most intense moments of the battle!");
            Console.WriteLine("Invincible Gorthaur the Malevolent is coming");
            Console.WriteLine("The battle begins!");

            // Flag to check if the player has used the run away option
            var isRunAwayUsed = false;

            // Main battle loop
            while (true)
            {
                // Player's turn
                await PlayerTurnAsync(player, enemy);
                if (enemy.Health <= 0)
                {
                    Console.WriteLine($"{enemy.Name} has been defeated! {player.Name} wins. Congratulations!");
                    break;
                }

                // Enemy's turn
                await EnemyTurnAsync(enemy, player);

                if (player.Health <= 0)
                {
                    Console.WriteLine($"{player.Name} has been defeated! {enemy.Name} wins. Congratulations!");
                    break;
                }

                // Checking if the player's health is low and if the run away option hasn't been used yet
                if (player.Health <= 50 && !isRunAwayUsed)
                {
                    await PlayerRunAwayAsync(player);
                    isRunAwayUsed = true;
                }

                // Displaying health of both combatants
                Console.WriteLine($"Current health of {player.Name}: {player.Health}");
                Console.WriteLine($"Current health of {enemy.Name}: {enemy.Health}");
                Console.WriteLine();

                // Adding delay to simulate some time between turns
                await Task.Delay(1000);
            }

            // Displaying battle results
            Console.WriteLine("Battle ended!");
            Console.WriteLine($"Player Miss Count: {player.MissCount}");
            Console.WriteLine($"Enemy Miss Count: {enemy.MissCount}");
        }

        // player's turn
        private async Task PlayerTurnAsync(Combatant player, Combatant enemy)
        {
            AttackType attackChoice;

            // Prompting the player to choose an attack type
            while (true)
            {
                Console.WriteLine($"Choose your attack type (Basic/Special): ");
                string? input = Console.ReadLine()?.ToLower();

                if (Enum.TryParse(input, true, out attackChoice) && Enum.IsDefined(typeof(AttackType), input))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid attack type. Please enter Basic or Special.");
                }
            }

            // Simulating a chance to miss the attack
            await Task.Delay(100);
            if (random.Next(0, 10) < 2)
            {
                Console.WriteLine("You missed!");
                player.IncrementMissCount();
            }
            else
            {
                // Calculating damage and applying it to the enemy
                int damage = CalculateDamage(player, enemy, attackChoice);
                enemy.Health -= damage;
                Console.WriteLine($"{player.Name} attacks {enemy.Name} with {attackChoice} attack and deals {damage} damage.");
            }
        }

        // enemy's turn
        private async Task EnemyTurnAsync(Combatant enemy, Combatant player)
        {
            AttackType attackChoice = random.Next(0, 2) == 0 ? AttackType.basic : AttackType.special;

            // Simulating a chance to miss the attack
            await Task.Delay(100);
            if (random.Next(0, 10) < 2)
            {
                Console.WriteLine("Enemy missed!");
                enemy.IncrementMissCount();
            }
            else
            {
                // Calculating damage and applying it to the player
                int damage = CalculateDamage(enemy, player, attackChoice);
                player.Health -= damage;
                Console.WriteLine($"{enemy.Name} attacks {player.Name} with {attackChoice} attack and deals {damage} damage.");
            }
        }

        // Method for player's run away option
        private static async Task PlayerRunAwayAsync(Combatant player)
        {
            string? choice = "";

            // Prompting the player to choose whether to run away
            while (string.IsNullOrWhiteSpace(choice) || (choice != "y" && choice != "n"))
            {
                Console.WriteLine($"Do you want to run away? (Y/N)");
                choice = Console.ReadLine()?.ToLower();

                if (string.IsNullOrWhiteSpace(choice) || (choice != "y" && choice != "n"))
                {
                    Console.WriteLine("Your choice is invalid. Please try again.");
                }
            }

            // If the player chooses to run away
            await Task.Delay(100);
            if (choice == "y")
            {
                // Prompting the player to buy a health elixir
                Console.WriteLine($"The merchant approached you and asked the following: 'I have a health elixir. Would you like to buy it?' (Y/N) (The elixir will add 10 points to your health.)");
                choice = Console.ReadLine()?.ToLower();
                if (choice == "y")
                {
                    player.Health += 10;
                }
            }
        }

        // Method to calculate damage based on the attacker's attack type and defender's armor
        private int CalculateDamage(Combatant attacker, Combatant defender, AttackType attackType)
        {
            int baseDamage = 0;

            // Generating base damage based on attack type
            if (attackType == AttackType.basic)
            {
                baseDamage = random.Next(5, 16);
            }
            else if (attackType == AttackType.special)
            {
                baseDamage = random.Next(15, 26);
            }

            // Calculating final damage after considering defender's armor
            int damage = Math.Max(0, baseDamage - defender.Armor);

            return damage;
        }
    }
}

