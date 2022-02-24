using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitChallenge
{
    internal class RabbitInitialize
    {
    }

    class RabbitSetup {

        private List<Boolean> holes = new List<Boolean>();
        Random Rand = new Random();
        int rabbitPosition;
        
        int userGuess;
        Boolean GameIsRunning = true;

        public RabbitSetup(int amountOfHoles)
        {
            //Fill list with amount of False values to represent the empty spaces that the rabbit is not in
            fillHoles(amountOfHoles);

            //Randomly turn one of the indexes to True to represent the index the rabbit occupies
            rabbitPosition = Rand.Next(holes.Count); // Get random val to serve as the initial spawning point of the rabbit

            holes[rabbitPosition] = true;
            Console.WriteLine("Rabbit is at: " + (rabbitPosition));
        }

        private void fillHoles(int amount) 
        { 
            for (int i = 0; i < amount; i++)
            {
                holes.Add(false);
            }
        }

        //Displays the location of the rabbit and the other holes
        public void displayHoles()
        {
            foreach (Boolean b in holes)
            {
                if (b)
                {
                    Console.Write("R" + " ");
                }
                else {
                    Console.Write("_" + " ");
                }
            }
        }

        //Displays only holes and hides the rabbit's location
        public void displayHolesHidden()
        {
            foreach (Boolean b in holes)
            {
                Console.Write("_");
            }
        }

        public int GetPlayerGuess()
        {
            string rawStringInput;
            int guess = -1;
            while (true)
            {
                try
                {
                    rawStringInput = Console.ReadLine();
                    guess = Convert.ToInt32(rawStringInput);

                    if (guess < 0)
                    {
                        Console.WriteLine("Guess again.");
                        throw new Exception("Your guess is too low.");
                    }

                    if (guess > holes.Count)
                    {
                        Console.WriteLine("Guess again.");
                        throw new Exception("Your guess is too high");
                    }

                    // If everything goes well, then we'll break out of the while loop
                    break;
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }

            //After integer conversion and range validation, return the guess integer
            return guess;
        }

        public void GameOrderManager()
        {
            while (true)
            {
                //Player goes first
                Console.WriteLine("Your turn:");
                PlayerTurn();

                //Check to see if game has ended due to the player's guess being correct
                if (GameIsRunning == false)
                {
                    break;
                }

                //If we have gotten this far, then the player has not guessed correctly
                //Rabbit will now move
                
                EnemyTurn();
                Console.WriteLine("Rabbit moves to: " + (rabbitPosition));
            }

            Console.WriteLine("Congratulations! You've found the rabbit!");
        }

        private void MoveRabbitLeft()
        {
            //Mark current rabbit position to false
            MarkPositionFalse(rabbitPosition);

            //Move rabbit to the left
            rabbitPosition--;

            //Mark it's new position (which is its current position now) to true
            MarkPositionTrue(rabbitPosition);
        }

        private void MoveRabbitRight()
        {
            //Mark current rabbit position to false
            MarkPositionFalse(rabbitPosition);

            //Move rabbit to the right
            rabbitPosition++;

            //Mark it's new position (which is its current position now) to true
            MarkPositionTrue(rabbitPosition);
        }

        private void MarkPositionFalse(int pos)
        {
            holes[pos] = false;
        }

        private void MarkPositionTrue(int pos)
        {
            holes[pos] = true;
        }

        public void PlayerTurn()
        {
            //Get player guess
            userGuess = GetPlayerGuess();

            //If user guesses the rabbit's spot correctly, then end the game
            if (holes[userGuess] == true)
            {
                GameIsRunning = false;
            }
        }

        //Rabbit can only move adjacently from its current position
        //Example: If rabbit is in P, then rabbit's next position can only be P + 1 or P - 1 
        public void EnemyTurn()
        {
            //If rabbit is at the edge of the holes, then the rabbit can only move in one direction, otherwise it can move in two directions
            //If the size of the holes are 10, then if rabbit is at position 0 or position 9, it can only move towards the center

            if (rabbitPosition == 0 || rabbitPosition == holes.Count)
            {
                if (rabbitPosition == 0)
                {
                    //Move rabbit's position to the right
                    MoveRabbitRight();
                }
                else
                {
                    //Move rabbit's position to the left
                    MoveRabbitLeft();
                }
            }
            else
            {
                var r = Rand.Next(2);

                if (r == 0)
                {
                    MoveRabbitRight();
                }
                else
                {
                    MoveRabbitLeft();
                }
            }
        }
        
    }
}
