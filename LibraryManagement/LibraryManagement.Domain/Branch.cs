
using LibraryManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagement.Domain
{
    public class Branch : AggregateRoot
    {
        /// <summary>
        /// Gets the name of the branch.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }
        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public Address Address { get; private set; }
        /// <summary>
        /// Gets the maximum inventory available for the branch.
        /// </summary>
        /// <value>
        /// The maximum inventory.
        /// </value>
        public int MaxInventory { get; private set; }
        /// <summary>
        /// Gets the total stock.
        /// </summary>
        /// <value>
        /// The total stock.
        /// </value>
        public int TotalStock => _bookInventory.Sum(x => x.TotalAvailable);
        private IList<BookStock> _bookInventory = new List<BookStock>();
        /// <summary>
        /// Gets the book inventory.
        /// </summary>
        /// <value>
        /// The book inventory.
        /// </value>
        public IList<BookStock> BookInventory { get { return _bookInventory; } }
        private IList<Member> _members = new List<Member>();
        /// <summary>
        /// Gets the members.
        /// </summary>
        /// <value>
        /// The members.
        /// </value>
        public IList<Member> Members { get { return _members; } }

        protected Branch()
        {
        }

        public Branch(string name, int maxInventory) : this()
        {
            Name = name;
            MaxInventory = maxInventory;
        }

        /// <summary>
        /// Enrols a new member to a branch.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <exception cref="System.InvalidOperationException">$Member - {member.Name} - already enroled at branch</exception>
        public void AddMember(Member member)
        {
            if (IsMemberEnroled(member))
                throw new InvalidOperationException($"Member - {member.Name} - already enroled at branch");

            _members.Add(member);
        }

        /// <summary>
        /// Creates a new inventory item within the branch for a book.
        /// </summary>
        /// <param name="inventoryItem">The inventory item.</param>
        /// <exception cref="System.InvalidOperationException">$Book - {inventoryItem.Book.Name} - already a stock item at this branch</exception>
        public void AddBookInventory(BookStock inventoryItem)
        {
            if (IsBookInInventory(inventoryItem.Book))
                throw new InvalidOperationException($"Book - {inventoryItem.Book.Name} - already a stock item at this branch");

            if (TotalStock + inventoryItem.TotalAvailable > MaxInventory)
                throw new OverflowException($"Adding this stock of books will take the amount of books above the maximum inventory available at branch - {Name} ");

            _bookInventory.Add(inventoryItem);
        }

        /// <summary>
        /// Adds the book to the branch's stock.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <exception cref="System.InvalidOperationException">$Book - {book.Name} - is not listed in the branch's inventory</exception>
        public void AddBookStock(Book book)
        {
            if (!IsBookInInventory(book))
                throw new InvalidOperationException($"Book - {book.Name} - is not listed in the branch's inventory");

            if (TotalStock + 1 > MaxInventory)
                throw new OverflowException($"Adding this stock of books will take the amount of books above the maximum inventory available at branch - {Name} ");

            FindBookInventory(book).AddOne();
        }

        /// <summary>
        /// Removes the book from the branch's stock list.
        /// </summary>
        /// <param name="book">The book.</param>
        public void RemoveBookStock(Book book)
        {
            CheckInventory(book);

            var inventoryItem = FindBookInventory(book);

            if(inventoryItem.TotalAvailable - 1 < 0)
                throw new OverflowException($"removing one of book - {book.Name} from available stock will take the amount of books below 0");

            inventoryItem.RemoveOne();
        }

        /// <summary>
        /// Validates if the book is in the branch's inventory.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <exception cref="System.InvalidOperationException">$Book - {book.Name} - is not listed in the branch's inventory</exception>
        private void CheckInventory(Book book)
        {
            if (!IsBookInInventory(book))
                throw new InvalidOperationException($"Book - {book.Name} - is not listed in the branch's inventory");
        }

        /// <summary>
        /// Determines whether [the book is already in the inventory] [the specified book].
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns></returns>
        public bool IsBookInInventory(Book book) => BookInventory.Any(x => x.Book.Id == book.Id);

        /// <summary>
        /// Determines whether [member is enroled at the branch] [the specified member].
        /// </summary>
        /// <param name="member">The member.</param>
        /// <returns></returns>
        public bool IsMemberEnroled(Member member) => _members.Any(x => x.Id == member.Id);

        public bool IsBookInStock(BookStock bookInventory) => (bookInventory.TotalAvailable - Members.SelectMany(x => x.AllocationHistory.Where(a => a.Book.Id == bookInventory.Book.Id && !a.ReturnedOn.HasValue)).Count()) > 0;
        /// <summary>
        /// Allocates the book to a member of the branch.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <param name="member">The member.</param>
        public void AllocateBook(Book book, Member member)
        {
            CheckInventory(book);

            var inventoryItem = FindBookInventory(book);

            if (inventoryItem.TotalAvailable == 0)
                throw new InvalidOperationException($"No stock available for book - {book.Name}");

            if (!IsMemberEnroled(member))
                throw new InvalidOperationException($"Member - {member.Name} - is not enroled at this branch");

            if (!IsBookInStock(inventoryItem))
                throw new InvalidOperationException($"{book.Name} is out of stock and cannot be allocated");

            member.AllocationHistory.Add(new BookAllocation(member, book, DateTime.Now));
        }

        /// <summary>
        /// Finds the book in the branch's inventory.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns></returns>
        private BookStock FindBookInventory(Book book) => _bookInventory.First(x => x.Book.Id == book.Id);
    }
}