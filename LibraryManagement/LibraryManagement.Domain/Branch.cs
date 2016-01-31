using LibraryManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagement.Domain
{
    public class Branch : AggregateRoot
    {
        public string Name { get; private set; }
        public int MaxInventory { get; private set; }
        private IList<BookStock> _bookInventory = new List<BookStock>();
        public IList<BookStock> BookInventory { get { return _bookInventory; } }
        private IList<Member> _members = new List<Member>();
        public IList<Member> Members { get { return _members; } }

        protected Branch()
        {
        }

        public Branch(string name, int maxInventory) : this()
        {
            Name = name;
            MaxInventory = maxInventory;
        }

        public void AddMember(Member member)
        {
            if (_members.Any(x => x.Id == member.Id))
                throw new InvalidOperationException("Member already enroled at branch");

            _members.Add(member);
        }

        public void AddBookInventory(BookStock inventoryItem)
        {
            if (IsBookInInventory(inventoryItem.BookId))
                throw new InvalidOperationException("Book already a stock item at this branch");

            _bookInventory.Add(inventoryItem);
        }

        public void AddBookStock(int bookId)
        {
            if (!IsBookInInventory(bookId))
                throw new InvalidOperationException("Book is not listed in the branch's inventory");

            FindBookInventory(bookId).TotalInStock++;
        }

        public void RemoveBookStock(int bookId)
        {
            if (!IsBookInInventory(bookId))
                throw new InvalidOperationException("Book is not listed in the branch's inventory");

            FindBookInventory(bookId).TotalInStock--;
        }

        public bool IsBookInInventory(int bookId) => BookInventory.Any(x => x.BookId == bookId);

        private BookStock FindBookInventory(int bookId) => _bookInventory.First(x => x.BookId == bookId);
    }
}