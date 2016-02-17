using LibraryManagement.Shared;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagement.Domain
{
    /// <summary>
    /// Holds information about Genre reference data
    /// </summary>
    public class Genre : Entity
    {
        public static readonly Genre None = new Genre(0, "None");
        public static readonly Genre ChildrensFiction = new Genre(1, "Children's Fiction");

        public string Name { get; private set; }

        protected Genre()
        {
        }

        public Genre(int id, string name) : this()
        {
            Name = name;
        }

        public static List<Genre> GetAllGenres()
        {
            return new List<Genre>
            {
                None,
                ChildrensFiction
            };
        }

        public static Genre GetGenreByName(string name) => GetAllGenres().First(x => x.Name == name);
    }
}