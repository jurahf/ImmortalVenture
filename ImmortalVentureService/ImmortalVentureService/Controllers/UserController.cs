using ImmortalVentureService.DTO;
using ImmortalVentureService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ImmortalVentureService.Controllers
{
    public class UserController : ApiController
    {
        private DBWork db = new DBWork();

        [HttpGet]
        [ActionName("GetUser")]
        public UserDto GetUser(int id)
        {
            var user = db.GetFromDatabase<Пользователь>(x => x.Id == id).FirstOrDefault();

            if (user == null)
                return null;

            return new UserDto(user);
        }


        [HttpPost]
        [ActionName("AddUser")]
        public int AddUser([FromBody] UserDto dto)
        {
            if (dto == null)
                return 0;

            Пользователь user = new Пользователь()
            {
                ФИО = dto.FIO,
                Логин = dto.Login,
                ЭП = dto.EP,
                ХэшПароля = dto.PasswordHash    // TODO
            };

            db.Insert(user);
            return user.Id;
        }



    }
}
