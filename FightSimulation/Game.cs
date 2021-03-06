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
        //Game over is set to false
        bool gameOver = false;

        //Current fighters
        Monster currentMonster1;
        Monster currentMonster2;

        int currentMonsterIndex = 0;
        int currentScene = 0;

        //Monster 1
        Monster wompus;
        //Monster 2
        Monster thwompus;
        //Monster 3
        Monster backupWompus;
        //Monster 4
        Monster unclePhil;
        //Array for all 4 monsters
        Monster[] monsters;

        public void Run()
        {
            Start();

            while(!gameOver)
            {
                Update();
            }

            End();
        }

        void End()
        {
            Console.WriteLine("Goodbye Friend!");
        }

        /// <summary>
        /// Initializes everything at the start of the game
        /// </summary>
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

            monsters = new Monster[] { wompus, thwompus, backupWompus, unclePhil };

            ResetCurrentMonsters();
        }

        /// <summary>
        /// Resets current fighters to be the first two monsters in the array
        /// </summary>
        void ResetCurrentMonsters()
        {
            //Current monster index is set to 0
            currentMonsterIndex = 0;

            //Set starting fighters
            currentMonster1 = monsters[currentMonsterIndex];
            currentMonsterIndex++;
            currentMonster2 = monsters[currentMonsterIndex];
        }

        /// <summary>
        /// Updates current scene player is in.
        /// </summary>
        void UpdateCurrentScene()
        {
            switch (currentScene)
            {
                case 0:
                    DisplayStartMenu();
                    break;

                case 1:
                    Battle();
                    UpdateCurrentMonsters();
                    Console.ReadKey(true);
                    break;

                case 2:
                    DisplayRestartMenu();
                    break;

                default:
                    Console.WriteLine("Invalid scene index");
                    break;
            }

            /*
            //If current scene is 0...
            if(currentScene == 0)
            {
                //...displays start menu
                DisplayStartMenu();
            }
            //If current scene is 1...
            else if(currentScene == 1)
            {
                //...start battle simulation
                Battle();
                UpdateCurrentMonsters();
                Console.ReadKey(true);
            }
            //If current scene is 2...
            else if(currentScene == 2)
            {
                //...displays restart menu
                DisplayRestartMenu();
            }
            */
        }

        /// <summary>
        /// Gets an input from the player based on some decision
        /// </summary>
        /// <param name="description">The context for the decision</param>
        /// <param name="option1">The first choice the player has</param>
        /// <param name="option2">The second choice the player has</param>
        /// <param name="pauseInvalid">If true, the player must press a key to constinue after inputting
        /// an incorrect value</param>
        /// <returns>A number representing which of the two options was choosen. Returns 0 if an invalid input
        /// was recieved</returns>
        int GetInput(string description, string option1, string option2, bool pauseInvalid = false)
        {
            //Print the context and options
            Console.WriteLine(description);
            Console.WriteLine("1. " + option1);
            Console.WriteLine("2. " + option2);

            //Get player input
            string input = Console.ReadLine();
            int choice = 0;

            //If the player typed 1...
            if(input == "1")
            {
                //...set the return variable to be 1
                choice = 1;
            }
            //IF the player typed 2...
            else if(input == "2")
            {
                //...set the return variable to be 2
                choice = 2;
            }
            //Otherwise the player did not type a 1 or 2...
            else
            {
                //...let them know the input was invalid
                Console.WriteLine("Invalid Input");

                //If we want to pause when an invalid input was recieved...
                if(pauseInvalid)
                {
                    //...make the player press a key to continue
                    Console.ReadKey(true);
                }
            }

            //Return the player choice
            return choice;
        }

        /// <summary>
        /// Displays the starting menu. Gives the player the option to start or exit the simulation.
        /// </summary>
        void DisplayStartMenu()
        {
            //Get player choice
            int choice = GetInput("Welcome to Monster Fight Simulator and Uncle Phil", "Start Simulation", "Quit");

            //If they chose to start the simulation...
            if(choice == 1)
            {
                //...start the battle scene
                currentScene = 1;
            }
            //Otherwise if they chose to exit...
            else if(choice == 2)
            {
                //...end the game
                gameOver = true;
            }
        }

        /// <summary>
        /// Displays the restart menu. Gives the player the option to restart or exit the simulation.
        /// </summary>
        void DisplayRestartMenu()
        {
            //Get the player choice
            int choice = GetInput("Simulation Over. Would you like to play again?", "Yes", "No");

            //If the player chose to restart...
            if (choice == 1)
            {
                //...set the current scene to be the start menu
                ResetCurrentMonsters();
                currentScene = 0;
            }
            //If the player chose to exit...
            else if( choice == 2)
            {
                //...end the game
                gameOver = true;
            }
        }

        /// <summary>
        /// Called every game loop
        /// </summary>
        void Update()
        {
            UpdateCurrentScene();
            Console.Clear();
        }

        Monster GetMonster(int monsterIndex)
        {
            Monster monster;
            monster.name = "None";
            monster.attack = 1;
            monster.defense = 1;
            monster.health = 1;


            if (monsterIndex == 0)
            {
               monster = unclePhil;
            }
            else if (monsterIndex == 1)
            {
                monster = backupWompus;
            }
            else if (monsterIndex == 2)
            {
                monster = wompus;
            }
            else if(monsterIndex == 3)
            {
                monster = thwompus;
            }

            return monster;
        }

        /// <summary>
        /// Simulates one turn in the current monster fight
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

        bool TryEndSimulation()
        {
            bool simulationOver = currentMonsterIndex >= monsters.Length;

            if(simulationOver)
            {
                currentScene = 2;
            }

            return simulationOver;
        }

        /// <summary>
        /// Changes one of the current fighters to be the next in the list
        /// if it has died. Ends the game if all fighters in the list have been used.
        /// </summary>
        void UpdateCurrentMonsters()
        {
            /*
            //If either monster is set to "None" and the last monster has been set...
            if (currentMonsterIndex >= monsters.Length)
            {
                //...go to the restart menu
                currentScene = 2;
            }
            */
            //If monster 1 has died...
            if (currentMonster1.health <= 0)
            {
                //... increment the current monster index and swap out the monster
                currentMonsterIndex++;

                if(TryEndSimulation())
                {
                    return;
                }

                currentMonster1 = monsters[currentMonsterIndex];
            }
            //If monster 2 has died...
            if (currentMonster2.health <= 0)
            {
                //... increment the current monster index and swap out the monster
                currentMonsterIndex++;

                if(TryEndSimulation())
                {
                    return;
                }
                currentMonster2 = monsters[currentMonsterIndex];
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
