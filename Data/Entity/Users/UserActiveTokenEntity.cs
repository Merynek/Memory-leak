using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entity
{
    public class UserActiveTokenEntity
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }

        public UserActiveTokenEntity()
        { }
    }
}
