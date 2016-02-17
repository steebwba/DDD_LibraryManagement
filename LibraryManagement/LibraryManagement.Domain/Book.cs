using LibraryManagement.Shared;

namespace LibraryManagement.Domain
{
    public class Book : Entity
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the International Standard Book Number.
        /// </summary>
        /// <value>
        /// The isbn.
        /// </value>
        public string Isbn { get; private set; }

        /// <summary>
        /// Gets the genre.
        /// </summary>
        /// <value>
        /// The genre.
        /// </value>
        public Genre Genre { get; private set; }

        /// <summary>
        /// Gets the author.
        /// </summary>
        /// <value>
        /// The author.
        /// </value>
        public string Author { get; private set; }

        /// <summary>
        /// Gets the number of pages.
        /// </summary>
        /// <value>
        /// The number of pages.
        /// </value>
        public int NumberOfPages { get;  private set; }

        /// <summary>
        /// Gets the number of chapters.
        /// </summary>
        /// <value>
        /// The number of chapters.
        /// </value>
        public int NumberOfChapters { get; private set; }

        /// <summary>
        /// Gets the blurb.
        /// </summary>
        /// <value>
        /// The blurb.
        /// </value>
        public string Blurb { get;  private set; }

        protected Book()
        {
        }

        public Book(string name, string isbn, Genre genre, string author, int numberOfPages, int numberOfChapters, string blurb) : this()
        {
            Name = name;
            Isbn = isbn;
            Genre = genre;
            Author = author;
            NumberOfPages = numberOfPages;
            NumberOfChapters = numberOfChapters;
            Blurb = blurb;
        }

        //protected override bool EqualsCore(Book other)
        //{
        //    return Name.Trim().ToLower() == other.Name.Trim().ToLower()
        //        && Author.Trim().ToLower() == other.Author.Trim().ToLower()
        //        && Isbn.Trim().ToLower() == Isbn.Trim().ToLower()
        //        && Genre.Name.Trim().ToLower() == other.Genre.Name.Trim().ToLower();
        //}

        //protected override int GetHashCodeCore()
        //{
        //    unchecked
        //    {
        //        int hashCode = Name.Trim().Length;
        //        hashCode = (hashCode * 397) ^ Author.Trim().Length;
        //        hashCode = (hashCode * 397) ^ Isbn.Trim().Length;
        //        hashCode = (hashCode * 397) ^ Genre.Name.Trim().Length;
        //        return hashCode;
        //    }
        //}
    }
}