//Initialize challenge object
var startChallenge = new RabbitChallenge.RabbitSetup(10);

//Display 
startChallenge.displayHoles();


Console.WriteLine("\n\nGuess where the rabbit is:");
//Player turn
startChallenge.GameOrderManager();
