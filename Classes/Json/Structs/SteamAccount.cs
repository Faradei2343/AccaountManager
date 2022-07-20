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
        [Required(ErrorMessage = "Введите логин аккаунта")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Логин должен содержать только латинские символы и цифры")]
        [JsonProperty("login")]
        public string Login { get; set; }

        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Пароль должен содержать только латинские символы и цифры")]
        [Required(ErrorMessage = "Введите пароль от аккаунта")]
        [JsonProperty("password")]
        public string Password { get; set; }

        [Required(ErrorMessage ="Введите имя записи")]
        [StringLength(50, ErrorMessage = "Имя должно быть длинной от 10 до 50 символов", MinimumLength = 10)]
        [JsonProperty("name")]
        public string Name { get; set; }

        public SteamAccount(string name, string login, string password )
        {
            Name = name;
            Login = login;
            Password = password;
        }
    }
}
