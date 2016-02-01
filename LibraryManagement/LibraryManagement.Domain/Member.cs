
using LibraryManagement.Domain.Common;
using System.Collections.Generic;

namespace LibraryManagement.Domain
{
    public class Member : Entity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public Address Address { get; private set; }
        private IList<BookAllocation> _allocationHistory = new List<BookAllocation>();
        public IList<BookAllocation> AllocationHistory => _allocationHistory;
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