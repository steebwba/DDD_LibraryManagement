using LibraryManagement.Core;

namespace LibraryManagement.Domain
{
    public class Address : ValueObject
    {
        public string AddressLine1 { get; private set; }
        public string AddressLine2 { get; private set; }
        public string AddressLine3 { get; private set; }
        public string City { get; private set; }
        public string County { get; private set; }
        public string PostCode { get; private set; }

        protected Address()
        {
        }

        public Address(string addressLine1, string addressLine2, string addressLine3, string city, string county, string postCode) : this()
        {
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            AddressLine3 = addressLine3;
            City = city;
            County = county;
            PostCode = postCode;
        }
    }
}