using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Server
{
    [Table(Name = "Files")]
    public class CaesFile
    {
        [Column(IsPrimaryKey = true)]
        public string Path;
        [Column]
        public string Name;

        public CaesFile()
        {

        }

        public CaesFile(String path, String name)
        {
            this.Path = path;
            this.Name = name;
        }

    }

    [Table(Name = "PendingMail")]
    public class Mail
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID;
        [Column]
        public string To;
        [Column]
        public string From;
        [Column]
        public string Message;

        public Mail()
        {

        }

        public Mail(String toUsername, String fromUsername, String message)
        {
            this.To = toUsername;
            this.From = fromUsername;
            this.Message = message;
        }

        public Mail(User toUsername, User fromUsername, String message) : this(toUsername.Username, fromUsername.Username, message)
        {
        }

    }

    [Table(Name = "Tags")]
    public class Tag
    {
        [Column(IsPrimaryKey = true)]
        public String FilePath;
        [Column(IsPrimaryKey = true)]
        public String TagName;

        public Tag()
        {

        }

        public Tag(CaesFile file, TagNames tag)
        {
            this.FilePath = file.Path;
            this.TagName = tag.TagName;
        }

    }

    [Table(Name = "TagNames")]
    public class TagNames
    {
        [Column(IsPrimaryKey = true)]
        public String TagName;

        public TagNames()
        {

        }

        public TagNames(String tag)
        {
            this.TagName = tag;
        }

    }

    [Table(Name = "Owns")]
    public class Owner
    {
        [Column(IsPrimaryKey = true)]
        public String Username;
        [Column(IsPrimaryKey = true)]
        public String FilePath;

    }


}
