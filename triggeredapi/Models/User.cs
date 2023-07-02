using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace triggeredapi.Models
{
    public class User: IdentityUser<Guid>
    {
        public string TelegramId {get; set;}
        public List<Novel> Novels {get; set; }
    }
}