using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Caesura
{

    [Table(Name = "Users")]
    public class User
    {
        private String name;
        private String password;


        [Column(IsPrimaryKey = true)]
        public string Username;
        [Column]
        public string PasswordHash;

        public User()
        {
        }




        internal void setName(String p)
        {
            if (p == null)
                throw new ArgumentNullException("Name can't be null");
            this.name = p.ToString();
        }

        internal String getName()
        {
            return this.name;
        }

        internal void setPass(String p)
        {
            if (p == null)
                throw new ArgumentNullException("Pass can't be null");
            this.password = p.ToString();
        }

        internal object getPass()
        {
            return this.password;
        }

        /*        internal bool writeNP()
                {
                    StreamWriter writer = File.AppendText("c:\\Users.txt");
            
                    try{
                        writer.WriteLine(this.name + ' ' + this.password);
                        writer.Close();
                    return true;
                    }

                    catch
                    {
                        return false;
                    }
                }*/
    }
}
