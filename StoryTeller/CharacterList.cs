using System;
using System.Collections.Generic;
using System.Linq;

namespace StoryTeller
{
    class CharacterList<T>
    {
        Dictionary<T, ConsoleColor> Characters = new Dictionary<T, ConsoleColor>();

        public void AddCharacter(T name, string color)
        {
            try
            {
                Characters[name] =
                        (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color, true);
            }
            catch (Exception)
            {
                Characters[name] = TheTeller.DefaultColor;
            }
        }
        public ConsoleColor GetCharactersColor(T Kname)
        {
            return Characters.Keys.Contains(Kname) ? (Characters[Kname]) : (TheTeller.DefaultColor);
        }
    }
}
