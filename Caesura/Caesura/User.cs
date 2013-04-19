using System;
<<<<<<< HEAD
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Caesura
{
    public class User
    {
        private String name;
        private String password;


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
