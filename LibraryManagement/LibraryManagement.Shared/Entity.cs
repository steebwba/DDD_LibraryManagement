using System;

namespace LibraryManagement.Shared{ 
    public class Entity
    {
        public Guid Id { get; set; }
        public bool Deleted { get; set; }
    }
}