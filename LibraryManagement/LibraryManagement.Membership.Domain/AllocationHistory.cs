using LibraryManagement.Shared;
using System;

namespace LibraryManagement.Membership.Domain
{
    public class AllocationHistory : Entity
    {
        public Guid BranchId { get; private set; }

        /// <summary>
        /// Gets the book identifier.
        /// </summary>
        /// <value>
        /// The book identifier.
        /// </value>
        public Guid BookId { get; private set; }

        /// <summary>
        /// Gets the allocated on.
        /// </summary>
        /// <value>
        /// The allocated on.
        /// </value>
        public DateTime AllocatedOn { get; private set; }

        /// <summary>
        /// Gets the date the book was returned on.
        /// </summary>
        /// <value>
        /// The returned on.
        /// </value>
        public DateTime? ReturnedOn { get; private set; }

        protected AllocationHistory() { }

        public AllocationHistory(Guid branchId, Guid bookId, DateTime allocatedOn, DateTime? returnedOn)
        {
            BranchId = branchId;
            BookId = bookId;
            AllocatedOn = allocatedOn;
            ReturnedOn = returnedOn;
        }

        public AllocationHistory(Guid branchId, Guid bookId, DateTime allocatedOn)
        {
            BranchId = branchId;
            BookId = bookId;
            AllocatedOn = allocatedOn;
        }
    }
}
