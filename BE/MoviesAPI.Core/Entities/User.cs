using MiNET.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Core.Entities
{
    public class User : Base
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string FirebaseId { get; set; }
        public List<FavoriteMovie> Movies { get; set; }
        public List<Comment> Comments { get; set; }

        public User(string firebaseId,string Name,string Email)
        {
            this.FirebaseId = firebaseId;
            this.Name = Name;
            this.Email = Email;
        }

        public static User CreateUser(string firebaseId,string Name,string Email)
        {
            return new User(firebaseId,Name,Email);
        }
    }
}
