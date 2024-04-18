using System.Numerics;

namespace BeratDemirel_215040088_GameProgramming_MidTerm
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Creating an enemy combatant with a name, health, level, and armor
            Combatant enemy = new("Gorthar the Malevolent", 100, 1, 3);

            // Creating a player combatant with initial values; the name will be set by the player later
            Combatant player = new("", 100, 1, 2);

            // Creating a Battle instance
            Battle battle = new Battle();

            // Starting the battle asynchronously with the player and enemy combatants
            await battle.StartBattleAsync(player, enemy);
        }
    }
}