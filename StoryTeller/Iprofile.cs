namespace StoryTeller
{
    //interface with likeminimal requirment for my class
    interface IProfile
    {
        string Name { get; set; }
        void vChangeName(string newName);
    }
}
