using System;
using System.Collections.Generic;
using System.Text;

namespace FightSimulation
{

    struct Monster
    {
        public string name;
        public float health;
        public float attack;
        public float defense;
    }
    class Game
    {
        public void Run()
        {
            //Monster 1
            Monster monster1;
            monster1.name = "Wompus";
            monster1.attack = 10.0f;
            monster1.defense = 5.0f;
            monster1.health = 20.0f;

            //Monster 2
            Monster monster2;
            monster2.name = "Thwompus";
            monster2.attack = 10.0f;
            monster2.defense = 5.0f;
            monster2.health = 20.0f;

            //Print monster1 stats
            PrintStats(monster1);
            //Print monster2 stats
            PrintStats(monster2);

            //Monster 1 attacks monster 2
            float damageTaken = Fight(monster1, monster2);
            Console.WriteLine(monster2.name + " has taken " + damageTaken);

            //Monster2 attacks monster1
            damageTaken = Fight(monster2, monster1);
            Console.WriteLine(monster1.name + " has taken " + damageTaken);

            Console.ReadKey();
            Console.Clear();

            //Print monster1 stats
            PrintStats(monster1);
            //Print monster2 stats
            PrintStats(monster2);
            Console.ReadKey();

        }

        /// <summary>
        /// Function that displays monsters stats
        /// </summary>
        /// <param name="name">Monsters name</param>
        /// <param name="attack">Monsters attack damage</param>
        /// <param name="defense">Monsters defense</param>
        /// <param name="health">Monsters health</param>
        void PrintStats(Monster monster)
        {
            Console.WriteLine("Name: " + monster.name);
            Console.WriteLine("Health: " + monster.health);
            Console.WriteLine("Attack: " + monster.attack);
            Console.WriteLine("Defense: " + monster.defense);
        }

        /// <summary>
        /// Calculates damage taken by subtracting attack from defense
        /// </summary>
        /// <param name="attack">Monsters attack value</param>
        /// <param name="defense">Monsters defense value</param>
        /// <returns></returns>
        float CalculateDamage(float attack, float defense)
        {
            float damage = attack - defense;

            if(damage <= 0)
            {
                damage = 0;
            }

            return damage;
        }

        float CalculateDamage(Monster attacker, Monster defender)
        {
            return attacker.attack - defender.defense;
        }

        float Fight(Monster attacker, Monster defender)
        {
            float damageTaken = CalculateDamage(attacker,defender);
            defender.health -= damageTaken;
            return damageTaken;
        }
    }
}
