using System;
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Data
{
    public class User
    {
        [Key] public int ID { get; set; }
        [DataType(DataType.Text)] public string Password { get; set; }
        [DataType(DataType.EmailAddress)] public string Email { get; set; }
        [DataType(DataType.Text)] public string Name { get; set; }
        
    }
}