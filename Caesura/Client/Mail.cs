using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Mail
    {
        public String username { get; set; }
        public String message { get; set; }
        public int id { get; set; }

        public Mail(String sender, String mail, int idnum)
        {
            username = sender;
            message = mail;
            id = idnum;
        }

        public override string ToString()
        {
            return username + "\n" + message + "\n" + id.ToString();
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Mail m = obj as Mail;
            if ((System.Object)m == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (username == m.username) && (message == m.message) && (id == m.id);
        }

        public override int GetHashCode()
        {
            return (username + message + id.ToString()).GetHashCode();
        }
    }
}

