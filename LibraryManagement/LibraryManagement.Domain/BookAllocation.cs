using LibraryManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain
{
    public class BookAllocation : Entity
    {
        public virtual Member Member { get; private set; }
        public virtual Book Book { get; private set; }
        public DateTime AllocatedOn { get; private set; }
        public DateTime? ReturnedOn { get; private set; }
        public BookAllocation(Member member, Book book, DateTime allocatedOn, DateTime? returnedOn)
        {
            Member = member;
            Book = book;
            AllocatedOn = allocatedOn;
            ReturnedOn = returnedOn;
        }

        public BookAllocation(Member member, Book book, DateTime allocatedOn)
        {
            Member = member;
            Book = book;
            AllocatedOn = allocatedOn;
        }
    }
}
