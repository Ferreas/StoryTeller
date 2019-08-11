namespace StoryTeller
{
    //tellers class from abstract person,currently implements only namechanging feature
    class Teller : APerson
    {
        public Teller(string name) : base(name)
        {
            TheTeller.TellersName = name;
        }

        public override void vChangeName(string newName)
        {
            Name = newName;
            TheTeller.TellersName = newName;
        }
    }
}
