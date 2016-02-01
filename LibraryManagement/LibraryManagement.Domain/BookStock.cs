using LibraryManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain
{
    public class BookStock : Entity
    {
        /// <summary>
        /// Gets the book.
        /// </summary>
        /// <value>
        /// The book.
        /// </value>
        public virtual Book Book { get; private set; }
        /// <summary>
        /// Gets the total books available in stock.
        /// </summary>
        /// <value>
        /// The total available.
        /// </value>
        public int TotalAvailable { get; private set; }

        protected BookStock() { }

        public BookStock(Book book, int quantity) : this()
        {
            TotalAvailable = quantity;
            Book = book;
        }

        public void AddOne() => TotalAvailable++;

        public void RemoveOne() => TotalAvailable--;
    }
}
