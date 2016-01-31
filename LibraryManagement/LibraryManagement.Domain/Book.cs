using LibraryManagement.Core;

namespace LibraryManagement.Domain
{
    public class Book : AggregateRoot
    {
        public string Name { get; private set; }
        public string Isbn { get; private set; }
        public Genre Genre { get; private set; }
        public string Author { get; private set; }
        public int NumberOfPages { get;  private set; }
        public int NumberOfChapters { get; private set; }
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

        public Book(string name, string isbn, Genre genre, string author, int numberOfPages, int numberOfChapters, string blurb, int totalInventory)
        {
            Name = name;
            Isbn = isbn;
            Genre = genre;
            Author = author;
            NumberOfPages = numberOfPages;
            NumberOfChapters = numberOfChapters;
            Blurb = blurb;
            TotalInventory = totalInventory;
        }
    }
}