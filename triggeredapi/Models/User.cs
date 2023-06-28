using System;
namespace triggeredapi.Models
{
    public class User
    {
        public Guid Id {get; set;}
        public string Username {get; set;}
        public string PasswordHash {get; set;}
        public string TelegramId {get; set;}
        public List<Novel> Novels {get; set; }
    }
}