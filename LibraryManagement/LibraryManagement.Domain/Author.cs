using LibraryManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain
{
    public class Author : Entity
    {
        public string Name { get; set; }

        protected Author() { }

        public Author(string name)
        {
            Name = name;
        }
    }
}
