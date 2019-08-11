using System;
using System.IO;



/*
 * ChangeLog goes here
 * Yes, I do know this isn't the right place for it
 * So...
 * 
 * New stuff:
 * +Now the game will greet us with default name and later we'll be able to change it
 * +Changed name now will replace default tellers name in OrdinaryText(string) 
 * 
 * Removed stuff:
 * 
 * Changed stuff:
 * ~Default folder for scripts is now documents->Storyteller. No more randomly stockpiling useless stuff in documents core folder.
 * ~Now default tellers name will be written in Cyan.
 */


namespace StoryTeller
{
    class Program
    {
        static void Main(string[] args)
        {
            MENU New_Game = new MENU();
            New_Game.ShowMenu();
        }
    }


    class TheTeller
    {
        static public string TellersName;
        //main and most important array carrying the whole story
        string[] script;
        //'Default' path to the array, may be changed in future
        internal int status = 0;
        // Set a variable to the Documents path.
        internal string docPath = Path.Combine(
          Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"StoryTeller");
        string ScriptFilePath = "";

        //Tries to load the script, if it doesn't exist shows the exception and closes the game
        public void LoadScript(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(Path.Combine(docPath, path), System.Text.Encoding.Default))
                {
                    script = sr.ReadToEnd().Split('\n');
                    ScriptFilePath = path;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Corrupted script file, unable to load. Press any key to exit the programm");
                Console.ReadKey();
                Environment.Exit(0);
            }

        }

        //Shows text on the screen like said by Narator or some charackter
        public void OrdinaryText(string ToSay)
        {
            switch (ToSay[0])
            {
                case '0':
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine(TellersName);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(ToSay.Substring(1));
                        
                    }
                    break;
                case '1':
                    {
                        Console.WriteLine(
                            ToSay.Substring(2, ToSay.IndexOf('"', 2) - 2)
                            + ":\n"
                            + ToSay.Substring(ToSay.IndexOf('"', 2) + 1)
                            );
                    }
                    break;
            }
        }

        //Counts points player got or lost while making decisions
        public bool Choise(string ToSay, string FirstA, string SecondA)
        {
            Console.WriteLine(ToSay.Substring(1));
            Console.WriteLine("1) " + FirstA.Substring(2));
            Console.WriteLine("2) " + SecondA.Substring(2));
            string chosen = Console.ReadLine();

            if (chosen == "1")
            {
                status += Convert.ToInt32(FirstA.Substring(0, 2));

                return true;
            }
            if (chosen == "2")
            {
                status += Convert.ToInt32(SecondA.Substring(0, 2));
                return true;
            }
            return false;
        }




        //The main unit proceeding through the script and reading the story
        public void ReadStory(int i = 0)
        {
            for (; i < script.Length;)
            {
                Console.Clear();
                switch (script[i][0])
                {
                    //Shows ordinary text on the screen(optional -with name)
                    case '0':
                        OrdinaryText(script[i].Substring(1));
                        i++;
                        break;
                    case '2':
                        //Asks the player to make decision and if it's made proceeds, else asks again
                        if (Choise(script[i], script[i + 1], script[i + 2]))
                        {
                            //jumps three lines through the script as two next are used for choises
                            i += 3;
                            continue;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Wrong Input!\nPress any key to try again.");
                            Console.ReadKey();
                            continue;
                        }
                        break;
                    case '5':
                        //Calculating the required to proceed points and loads next part of the story if conditions are met
                        double trigger = char.GetNumericValue(script[i][1]) * 10 + char.GetNumericValue(script[i][2]);
                        if (trigger == 0 || (status >= trigger && status != 0))
                        {
                            LoadScript(docPath+script[i].Substring(3, script[i].Length - 4));
                            i = 0;
                            status = 0;
                            continue;
                        }
                        else
                        //just proceeds to the next line
                        {
                            i++;
                            continue;
                        }
                        break;
                }
                AwaitKey(i, out i);
                //Exit to the menu
                if (i == -99)
                {
                    break;
                }
            }
        }

        //special keys handler
        public void AwaitKey(int ind, out int prev)
        {
            ConsoleKey PressedK = Console.ReadKey().Key;
            prev = ind;
            //To revind the story
            if (PressedK == ConsoleKey.LeftArrow)
            {
                if (ind != 1)
                {
                    if (script[ind - 2][0] == '0')
                    {
                        prev = ind - 2;
                    }
                }
                else
                    prev = ind - 1;
            }
            //To move to the next line
            else if (PressedK == ConsoleKey.RightArrow)
            {
                prev = ind;
            }
            else
            {
                //Save game handler
                if (PressedK == ConsoleKey.S)
                {
                    SaveGame(ind);
                    Console.WriteLine("Game saved");
                }
                //Exit handler
                else if (PressedK == ConsoleKey.E)
                {
                    prev = -99;
                }
                //Loop for the wrong pressed buttons
                else
                    AwaitKey(ind, out prev);
            }
        }

        //Saves current progress to special txt file in documents folder
        public void SaveGame(int position)
        {
            string[] lines = { ScriptFilePath, position.ToString(), status.ToString() };

            File.WriteAllLines(Path.Combine(docPath, "SaveGame.txt"), lines);
        }
    }

    
}
