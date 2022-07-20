using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AccaountManager.Classes.Json.Structs
{
    public class SteamAccount
    {
        [Required(ErrorMessage ="Введите имя записи")]
        [JsonProperty("name")]
        [StringLength(50,ErrorMessage ="Имя должно быть длинной от 10 до 50 символов", MinimumLength = 10)]
        public string Name { get; set; }
        [JsonProperty("login")]
        [Required(ErrorMessage ="Введите логин аккаунта")]
        public string Login { get; set; }
        [Required(ErrorMessage ="Введите пароль от аккаунта")]
        [JsonProperty("password")]
        public string Password { get; set; }

        public SteamAccount(string name, string login, string password )
        {
            Name = name;
            Login = login;
            Password = password;
        }
    }
}
