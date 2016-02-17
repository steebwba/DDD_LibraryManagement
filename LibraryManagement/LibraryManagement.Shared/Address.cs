namespace LibraryManagement.Shared
{
    public class Address : ValueObject<Address>
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

        protected override bool EqualsCore(Address other)
        {
            return AddressLine1.Trim().ToLower() == other.AddressLine1.Trim().ToLower()
                && AddressLine2.Trim().ToLower() == other.AddressLine2.Trim().ToLower()
                && AddressLine3.Trim().ToLower() == other.AddressLine3.Trim().ToLower()
                && City.Trim().ToLower() == other.City.Trim().ToLower()
                && County.Trim().ToLower() == other.County.Trim().ToLower()
                && PostCode.Trim().ToLower() == other.PostCode.Trim().ToLower();
        }

        protected override int GetHashCodeCore()
        {
            unchecked {
                int hashCode = AddressLine1.Trim().Length;
                hashCode = (hashCode * 397) ^ AddressLine2.Trim().Length;
                hashCode = (hashCode * 397) ^ AddressLine3.Trim().Length;
                hashCode = (hashCode * 397) ^ City.Trim().Length;
                hashCode = (hashCode * 397) ^ County.Trim().Length;
                hashCode = (hashCode * 397) ^ PostCode.Trim().Length;

                return hashCode;
            }
        }
    }
}