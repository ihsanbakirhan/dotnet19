using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{
    public class User
    {
        public Int64 userId { get; set; }
        public String userName { get; set; }
        public String email { get; set; }

        public User(Int64 userId, String userName, String email)
        {
            this.userId = userId;
            this.userName = userName;
            this.email = email;
        }
    }
}
