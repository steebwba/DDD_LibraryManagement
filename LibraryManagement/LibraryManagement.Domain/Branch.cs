using LibraryManagement.Shared;
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
        
        private IList<BookInventory> _bookInventory = new List<BookInventory>();
        /// <summary>
        /// Gets the book inventory.
        /// </summary>
        /// <value>
        /// The book inventory.
        /// </value>
        public IList<BookInventory> BookInventory => _bookInventory;

        /// <summary>
        /// Gets the total books in the branch's inventory.
        /// </summary>
        /// <value>
        /// The total books inventory.
        /// </value>
        public int TotalBooksInventory => _bookInventory.Sum(x => x.Total);

        private IList<Guid> _members = new List<Guid>();
        /// <summary>
        /// Gets the members.
        /// </summary>
        /// <value>
        /// The members.
        /// </value>
        public IList<Guid> Members => _members;

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
        public void AddMember(Guid member)
        {
            if (IsMemberEnroled(member))
                throw new InvalidOperationException($"Member ID - {member} - already enroled at branch");

            _members.Add(member);
        }

        /// <summary>
        /// Creates a new inventory item within the branch for a book.
        /// </summary>
        /// <param name="inventoryItem">The inventory item.</param>
        /// <exception cref="System.InvalidOperationException">$Book - {inventoryItem.Book.Name} - already a stock item at this branch</exception>
        public void AddBookInventory(BookInventory inventoryItem)
        {
            if (IsBookInInventory(inventoryItem.BookId))
                throw new InvalidOperationException($"Book - {inventoryItem.BookId} - already a stock item at this branch");

            if (TotalBooksInventory + inventoryItem.Total > MaxInventory)
                throw new OverflowException($"Adding this stock of books will take the amount of books above the maximum inventory available at branch - {Name} ");

            _bookInventory.Add(inventoryItem);
        }

        /// <summary>
        /// Adds the book to the branch's stock.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <exception cref="System.InvalidOperationException">$Book - {bookId} - is not listed in the branch's inventory</exception>
        /// <exception cref="System.OverflowException"></exception>
        public void AddBookStock(Guid bookId)
        {
            if (!IsBookInInventory(bookId))
                throw new InvalidOperationException($"Book - {bookId} - is not listed in the branch's inventory");

            if (TotalBooksInventory + 1 > MaxInventory)
                throw new OverflowException($"Adding this stock of books will take the amount of books above the maximum inventory available at branch - {Name} ");

            FindBookInventory(bookId).AddOneToInventory();
        }

        /// <summary>
        /// Removes the book from the branch's stock list.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <exception cref="System.OverflowException"></exception>
        public void RemoveBookStock(Guid bookId)
        {
            CheckInventory(bookId);

            var inventoryItem = FindBookInventory(bookId);

            if(inventoryItem.Total - 1 < 0)
                throw new OverflowException($"removing one of book - {bookId} from available stock will take the amount of books below 0");

            inventoryItem.RemoveOneFromInventory();
        }

        /// <summary>
        /// Validates if the book is in the branch's inventory.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <exception cref="System.InvalidOperationException">$Book - {bookId} - is not listed in the branch's inventory</exception>
        private void CheckInventory(Guid bookId)
        {
            if (!IsBookInInventory(bookId))
                throw new InvalidOperationException($"Book - {bookId} - is not listed in the branch's inventory");
        }

        /// <summary>
        /// Determines whether [the book is already in the inventory] [the specified book].
        /// </summary>
        /// <param name="bookId">The book.</param>
        /// <returns></returns>
        public bool IsBookInInventory(Guid bookId) => BookInventory.Any(x => x.BookId == bookId);

        /// <summary>
        /// Determines whether [member is enroled at the branch] [the specified member].
        /// </summary>
        /// <param name="member">The member.</param>
        /// <returns></returns>
        public bool IsMemberEnroled(Guid member) => _members.Any(x => x == member);

        /// <summary>
        /// Allocates the book to a member of the branch.
        /// </summary>
        /// <param name="bookId">The book.</param>
        /// <param name="member">The member.</param>
        /// <summary>
        /// Allocates the book.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <param name="memberId">The member identifier.</param>
        /// <exception cref="System.InvalidOperationException">
        /// </exception>
        /// <summary>
        /// Allocates the book.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <param name="memberId">The member identifier.</param>
        /// <exception cref="System.InvalidOperationException">
        /// </exception>
        public void AllocateBook(Guid bookId, Guid memberId)
        {
            CheckInventory(bookId);

            var inventoryItem = FindBookInventory(bookId);

            if (inventoryItem.Total == 0)
                throw new InvalidOperationException($"No stock available for book - {bookId}");

            if (!IsMemberEnroled(memberId))
                throw new InvalidOperationException($"Member - {memberId} - is not enroled at this branch");

            if (inventoryItem.TotalInStock == 0)
                throw new InvalidOperationException($"{bookId} is out of stock and cannot be allocated");

            ////TODO Put to member
            //member.AllocationHistory.Add(new BookAllocation(member, book, DateTime.Now));
        }

        /// <summary>
        /// Finds the book in the branch's inventory.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns></returns>
        private BookInventory FindBookInventory(Guid bookId) => _bookInventory.First(x => x.BookId == bookId);
    }
}