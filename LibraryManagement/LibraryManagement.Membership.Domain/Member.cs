using LibraryManagement.Shared;
using System.Collections.Generic;

namespace LibraryManagement.Membership.Domain
{
    public class Member : Entity
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; private set; }

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public Address Address { get; private set; }

        private IList<AllocationHistory> _allocationHistory = new List<AllocationHistory>();
        /// <summary>
        /// Gets the allocation history.
        /// </summary>
        /// <value>
        /// The allocation history.
        /// </value>
        public IList<AllocationHistory> AllocationHistory => _allocationHistory;

        protected Member()
        {
        }

        public Member(string name, string email, Address address) : this()
        {
            Name = name;
            Email = email;
            Address = address;
        }       

    }
}