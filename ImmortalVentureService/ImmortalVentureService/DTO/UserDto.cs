using ImmortalVentureService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImmortalVentureService.DTO
{
    public class UserDto
    {
        public string FIO { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string EP { get; set; }

        public UserDto(Пользователь user)
        {
            if (user == null)
                return;

            this.FIO = user.ФИО;
            this.Login = user.Логин;
            this.PasswordHash = user.ХэшПароля;
            this.EP = user.ЭП;
        }
    }
}