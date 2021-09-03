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
        bool gameOver = false;

        //Current fighters
        Monster currentMonster1;
        Monster currentMonster2;

        int currentMonsterIndex = 1;

        //Monster 1
        Monster wompus;
        //Monster 2
        Monster thwompus;
        //Monster 3
        Monster backupWompus;
        //Monster 4
        Monster unclePhil;

        public void Run()
        {
            Start();

            while(!gameOver)
            {
                Update();
            }
        }

        void Start()
        {
            //Initialize Monster
            wompus.name = "Wompus";
            wompus.attack = 15.0f;
            wompus.defense = 5.0f;
            wompus.health = 20.0f;


            thwompus.name = "Thwompus";
            thwompus.attack = 20.0f;
            thwompus.defense = 10.0f;
            thwompus.health = 15.0f;


            backupWompus.name = "Backup Wompus";
            backupWompus.attack = 25.6f;
            backupWompus.defense = 5.0f;
            backupWompus.health = 3.0f;


            unclePhil.name = "Uncle Phil";
            unclePhil.attack = 1000000;
            unclePhil.defense = 0;
            unclePhil.health = 1.0f;

            //Set starting fighters
            currentMonster1 = GetMonster(currentMonsterIndex);
            currentMonsterIndex++;
            currentMonster2 = GetMonster(currentMonsterIndex);
        }

        void Update()
        {
            Battle();
            UpdateCurrentMonsters();
            Console.ReadKey(true);
        }

        Monster GetMonster(int monsterIndex)
        {
            Monster monster;
            monster.name = "None";
            monster.attack = 1;
            monster.defense = 1;
            monster.health = 1;


            if (monsterIndex == 1)
            {
               monster = unclePhil;
            }
            else if (monsterIndex == 2)
            {
                monster = backupWompus;
            }
            else if (monsterIndex == 3)
            {
                monster = wompus;
            }
            else if(monsterIndex == 4)
            {
                monster = thwompus;
            }

            return monster;
        }

        /// <summary>
        /// Simulates one turn in the current conster fight
        /// </summary>
        void Battle()
        {
            //Print monster1 stats
            PrintStats(currentMonster1);
            //Print monster2 stats
            PrintStats(currentMonster2);

            //Monster 1 attacks monster 2
            float damageTaken = Fight(currentMonster1, ref currentMonster2);
            Console.WriteLine(currentMonster2.name + " has taken " + damageTaken);

            //Monster 2 attacks monster 1
            damageTaken = Fight(currentMonster2, ref currentMonster1);
            Console.WriteLine(currentMonster1.name + " has taken " + damageTaken);

            Console.ReadKey(true);
            Console.Clear();
        }

        /// <summary>
        /// Changes one of the current fighters to be the next in the list
        /// if it has died. Ends the game if all fighters in the list have been used.
        /// </summary>
        void UpdateCurrentMonsters()
        {
            //If monster 1 has died...
            if(currentMonster1.health <= 0)
            {
                //... increment the current monster index and swap out the monster
                currentMonsterIndex++;
                currentMonster1 = GetMonster(currentMonsterIndex);
            }
            //If monster 2 has died...
            if (currentMonster2.health <= 0)
            {
                //... increment the current monster index and swap out the monster
                currentMonsterIndex++;
                currentMonster2 = GetMonster(currentMonsterIndex);
            }
            //If either monster is set to "None" and the last monster has been set...
            if(currentMonster2.name == "None" || currentMonster1.name == "None" && currentMonsterIndex >= 4)
            {
                //...end the game
                Console.WriteLine("Simulation Over");
                gameOver = true;
            }

                  
        }

        string StartBattle(ref Monster monster1, ref Monster monster2)
        {
            string matchResult = "No Contest";

            while (monster1.health > 0 && monster2.health > 0)
            {
                //Print monster1 stats
                PrintStats(monster1);
                //Print monster2 stats
                PrintStats(monster2);

                //Monster 1 attacks monster 2
                float damageTaken = Fight(monster1, ref monster2);
                Console.WriteLine(monster2.name + " has taken " + damageTaken);

                //Monster 2 attacks monster 1
                damageTaken = Fight(monster2, ref monster1);
                Console.WriteLine(monster1.name + " has taken " + damageTaken);

                Console.ReadKey(true);
                Console.Clear();
            }

            if(monster1.health < 0 && monster2.health < 0)
            {
                matchResult = "Draw";
            }
            else if(monster1.health > 0)
            {
                matchResult = monster1.name;
            }
            else if(monster2.health > 0)
            {
                matchResult = monster2.name;
            }
           
            //Monster who the battle
            return matchResult;
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
        float CalculateDamage(Monster attacker, Monster defender)
        {
            return attacker.attack - defender.defense;
        }

        /// <summary>
        /// Calculates health after damage has been taken from the monster using defense
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="defender"></param>
        /// <returns></returns>
        float Fight(Monster attacker, ref Monster defender)
        {
            float damageTaken = CalculateDamage(attacker,defender);
            defender.health -= damageTaken;
            return damageTaken;
        }
    }
}
