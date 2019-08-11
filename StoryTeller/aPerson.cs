namespace StoryTeller
{
    //abstract class for person
    abstract class APerson : IProfile
    {
        public string Name { get; set; }

        public APerson(string name)
        {
            Name = name;
        }
        public abstract void vChangeName(string newName);
    }
}
