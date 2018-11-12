using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace testAdv.Models
{
    public class ChatContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
    }

    public class User
    {
        [Key]
        public Guid Guid { get; set; }
        public string Name { get; set; }
    }

    public class Message
    {

        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]
        public string DateStr {
            get { return Date.ToString("H:mm"); }
        }
        public string Author { get; set; }
        public string Text { get; set; }
    }
}