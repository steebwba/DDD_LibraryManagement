using LibraryManagement.Domain.Common;
using System;

namespace LibraryManagement.Domain
{
    public class Book : Entity
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