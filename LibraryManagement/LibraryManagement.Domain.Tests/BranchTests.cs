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
            Book testBook = new Book("TestBook", string.Empty, Genre.ChildrensFiction, string.Empty, 1, 1, string.Empty);


            testBranch.AddBookInventory(new BookStock(testBook, 1));

            testBranch.BookInventory.Should().HaveCount(1);
        }

        [Fact]
        public void Branch_Add_Single_Book_Already_In_Inventory()
        {
            Branch testBranch = new Branch("Test", 1);
            Book testBook = new Book("TestBook", string.Empty, Genre.ChildrensFiction, string.Empty, 1, 1, string.Empty);

            testBranch.AddBookInventory(new BookStock(testBook, 1));

            Xunit.Assert.Throws<InvalidOperationException>(() => testBranch.AddBookInventory(new BookStock(testBook, 1)));    
        }

        [Fact]
        public void Branch_Add_Book_Above_Max_Inventory()
        {
            Branch testBranch = new Branch("Test", 1);
            Book testBook = new Book("TestBook", string.Empty, Genre.ChildrensFiction, string.Empty, 1, 1, string.Empty);

            Xunit.Assert.Throws<OverflowException>(() => testBranch.AddBookInventory(new BookStock(testBook, 2)));
        }

        [Fact]
        public void Branch_Add_Book_Stock_Successfully()
        {
            Branch testBranch = new Branch("Test", 2);
            Book testBook = new Book("TestBook", string.Empty, Genre.ChildrensFiction, string.Empty, 1, 1, string.Empty);
            testBranch.AddBookInventory(new BookStock(testBook, 1));

            testBranch.AddBookStock(testBook);

            testBranch.BookInventory.Should().HaveCount(1);
            testBranch.BookInventory.First(x => x.Book.Name == "TestBook").TotalAvailable.Should().Be(2);
        }

        [Fact]
        public void Branch_Remove_Book_Stock_Successfully()
        {
            Branch testBranch = new Branch("Test", 2);
            Book testBook = new Book("TestBook", string.Empty, Genre.ChildrensFiction, string.Empty, 1, 1, string.Empty);
            testBranch.AddBookInventory(new BookStock(testBook, 2));

            testBranch.RemoveBookStock(testBook);

            testBranch.BookInventory.Should().HaveCount(1);
            testBranch.BookInventory.First(x => x.Book.Name == "TestBook").TotalAvailable.Should().Be(1);
        }

        [Fact]
        public void Branch_Remove_Book_No_Stock_Should_Throw_Error()
        {
            Branch testBranch = new Branch("Test", 1);
            Book testBook = new Book("TestBook", string.Empty, Genre.ChildrensFiction, string.Empty, 1, 1, string.Empty);
            testBranch.AddBookInventory(new BookStock(testBook, 0));

            Xunit.Assert.Throws<OverflowException>(() => testBranch.RemoveBookStock(testBook));
        }
    }
}
