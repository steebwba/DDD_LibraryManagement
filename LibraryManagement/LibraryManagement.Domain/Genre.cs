using LibraryManagement.Core;

namespace LibraryManagement.Domain
{
    public class Genre : Entity
    {
        public string Name { get; private set; }

        protected Genre()
        {
        }

        public Genre(string name) : this()
        {
            Name = name;
        }
    }
}