using LibraryManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain
{
    public class BookStock : Entity
    {
        public int BookId { get; private set; }
        public int TotalInStock { get; set; }

    }
}
