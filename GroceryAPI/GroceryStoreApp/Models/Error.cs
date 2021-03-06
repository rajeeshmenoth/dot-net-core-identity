using System;

namespace IdentityCore.Models
{
    public class Error
    {
        public String name { get; set; }
        public String description { get; set; }
        public int number { get; set; }

        public Error()
        {
        }

        public Error(String name, String description)
        {
            this.name = name;
            this.description = description;
        }
    }
}
