using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using LibraryManagement.Domain;
using FluentAssertions;
using System.Linq;

namespace LibraryManagement.Tests
{
    public class BranchTests
    {
        [Fact]
        public void Branch_Add_Single_Book_To_Inventory_Successfully()
        {
            Branch testBranch = new Branch("Test", 1);
            var bookId = Guid.NewGuid();

            testBranch.AddBookInventory(new BookInventory(bookId, 1, 1));

            testBranch.BookInventory.Should().HaveCount(1);
        }

        [Fact]
        public void Branch_Add_Single_Book_Already_In_Inventory()
        {
            Branch testBranch = new Branch("Test", 1);
            var bookId = Guid.NewGuid();

            testBranch.AddBookInventory(new BookInventory(bookId, 1, 1));

            Xunit.Assert.Throws<InvalidOperationException>(() => testBranch.AddBookInventory(new BookInventory(bookId, 1, 1)));    
        }

        [Fact]
        public void Branch_Add_Book_Above_Max_Inventory()
        {
            Branch testBranch = new Branch("Test", 1);
            var bookId = Guid.NewGuid();

            Xunit.Assert.Throws<OverflowException>(() => testBranch.AddBookInventory(new BookInventory(bookId, 2, 2)));
        }

        [Fact]
        public void Branch_Add_Book_Stock_Successfully()
        {
            Branch testBranch = new Branch("Test", 2);
            var bookId = Guid.NewGuid();

            testBranch.AddBookInventory(new BookInventory(bookId, 1, 1));
            testBranch.AddBookStock(bookId);
            testBranch.BookInventory.Should().HaveCount(1);
            testBranch.BookInventory.First(x => x.BookId == bookId).Total.Should().Be(2);
            testBranch.BookInventory.First(x => x.BookId == bookId).TotalInStock.Should().Be(2);
        }

        [Fact]
        public void Branch_Remove_Book_Stock_Successfully()
        {
            Branch testBranch = new Branch("Test", 2);
            var bookId = Guid.NewGuid();
            testBranch.AddBookInventory(new BookInventory(bookId, 2, 2));

            testBranch.RemoveBookStock(bookId);

            testBranch.BookInventory.Should().HaveCount(1);
            testBranch.BookInventory.First(x => x.BookId == bookId).TotalInStock.Should().Be(1);
            testBranch.BookInventory.First(x => x.BookId == bookId).Total.Should().Be(1);
        }

        [Fact]
        public void Branch_Remove_Book_No_Stock_Should_Throw_Error()
        {
            Branch testBranch = new Branch("Test", 1);
            var bookId = Guid.NewGuid();
            testBranch.AddBookInventory(new BookInventory(bookId, 0, 0));

            Xunit.Assert.Throws<OverflowException>(() => testBranch.RemoveBookStock(bookId));
        }
    }
}
