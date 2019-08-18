using System;

namespace StoryTeller
{
    //tellers class from abstract person,currently implements only namechanging feature
    class Teller : APerson
    {
        public Teller(string name) : base(name)
        {
            TheTeller.TellersName = name;
        }
        public Teller(string name, ConsoleColor color) : base(name, color)
        {
            TheTeller.TellersName = name;
        }
        public void vChangeName(string newName)
        {
            Name = newName;
            TheTeller.TellersName = newName;
        }

        public void SetTellersColor()
        {
            for (; ; )
            {
                try
                {
                    Console.WriteLine("Enter the Name of Color in Which do You Want to Print");
                    string str = Console.ReadLine();
                    Color = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), str, true);
                    Console.Write("Color successfully set to ");
                    Console.ForegroundColor = Color;
                    Console.WriteLine(Color.ToString());
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadKey(true);
                    break;
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Wrong Color Name, Try Again");
                }
            }
        }
    }
}
