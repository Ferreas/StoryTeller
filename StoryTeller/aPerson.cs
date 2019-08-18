using System;

namespace StoryTeller
{
    //abstract class for person
    abstract class APerson : IProfile
    {
        public string Name { get; set; }
        public ConsoleColor Color { get; set; } = ConsoleColor.White;
        public APerson(string name)
        {
            Name = name;
        }
        public APerson(string name, ConsoleColor color)
        {
            Name = name;
            Color = color;
        }
    }
}
