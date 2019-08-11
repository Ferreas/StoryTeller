using System;
using System.IO;

namespace StoryTeller
{
    class MENU : TheTeller
    {
        Teller teller = new Teller("Story Teller");
        //Shows the main menu of the game
        public void ShowMenu()
        {
            string selected = "";
            Console.Clear();
            Console.WriteLine(
                "=========THE STORYTELLER===========\n" +
                $"Hello {TellersName}\n\n" +
                "1)New Game\n" +
                "2)Load Game\n" +
                "3)Help\n" +
                "4)Options\n" +
                "5)Exit"
                );


            selected = Console.ReadLine();
            switch (selected)
            {
                //New game button handler
                case "1":
                    LoadScript(Path.Combine(base.docPath, "source.txt"));
                    base.status = 0;
                    ReadStory(0);
                    break;
                //Load game button handler
                case "2":
                    LoadGame();
                    break;
                //Help button handler
                case "3":
                    Console.Clear();
                    Console.WriteLine(
                        "Greetings, Traveler!\n\n" +
                        "This is an interactive novel\n" +
                        "As you read it you will be able to affect the flow of it\n\n\n" +
                        "To navigate through the story press LEFT and RIGHT keys\n" +
                        "If you made a dicision or passed vital point you won't be able to look back\n\n" +
                        "To save the game press S\n" +
                        "To Exit to main manu press E\n" +
                        "Loading the game is only possible from Main Menu\n\n" +
                        "Wish you a good time reading it!"
                        );
                    Console.ReadKey();
                    break;
                case "4":
                    Options();
                    break;
                //Exit button handler
                case "5":
                    Environment.Exit(0);
                    break;
            }
            //Loop
            ShowMenu();
        }

        //Oh well, here we go with some options to customize our storytelling machine
        private void Options()
        {
            Console.Clear();
            Console.WriteLine(
                   "1)Change Name\n" +
                   "2)Go Back"
                   );

            bool bLoop = true;
            string selected = Console.ReadLine();
            switch (selected)
            {
                //choosing new name for our "account"
                case "1":
                    Console.WriteLine($"Choose new name for {TellersName}");
                    teller.vChangeName(Console.ReadLine());
                    Console.WriteLine($"Name set to {TellersName}\nPress any key to continue");
                    Console.ReadKey();
                    break;
                    //going back...
                case "2":
                    bLoop = false;
                    break;
            }

            (bLoop ? (Action)Options:ShowMenu)();
        }






        //Load game 
        public void LoadGame()
        {
            //Temporary string array to allocate loading data
            string[] temp;
            //tries to load and starts the game with loaded preset if possible
            try
            {
                using (StreamReader sr = new StreamReader(Path.Combine(base.docPath, "SaveGame.txt"), System.Text.Encoding.Default))
                {
                    temp = sr.ReadToEnd().Split('\n');
                    LoadScript(temp[0].Substring(0, temp[0].Length - 1));
                    base.status = Convert.ToInt32(temp[2]);
                    ReadStory(Convert.ToInt32(temp[1]));
                }
            }
            //catches the exception and throws you to main menu if something went wrong
            catch (Exception)
            {
                Console.WriteLine("Corrupted save file, only new game possible now!");
                ShowMenu();
            }
        }

    }
}
