﻿using LibraryManagement.Shared;
using System;

namespace LibraryManagement.Domain
{
    public class BookAllocation : Entity
    {
        public Guid MemberId { get; private set; }

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

        protected BookAllocation() { }

        public BookAllocation(Guid memberId, Guid bookId, DateTime allocatedOn, DateTime? returnedOn)
        {
            MemberId = memberId;
            BookId = bookId;
            AllocatedOn = allocatedOn;
            ReturnedOn = returnedOn;
        }

        public BookAllocation(Guid memberId, Guid bookId, DateTime allocatedOn)
        {
            MemberId = memberId;
            BookId = bookId;
            AllocatedOn = allocatedOn;
        }
    }
}
