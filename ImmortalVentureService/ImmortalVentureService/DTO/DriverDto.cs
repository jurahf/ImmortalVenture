using ImmortalVentureService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImmortalVentureService.DTO
{
    public class DriverDto
    {
        public string Hash { get; set; }
        public int? UserId { get; set; }
        public UserDto User { get; set; }
        public Пол Gender { get; set; }
        public DateTime Birthday { get; set; }

        public DriverDto(Водитель driver)
        {
            if (driver == null)
                return;

            this.Hash = driver.ВнешнийХэш;
            this.UserId = driver.Пользователь?.Id;
            this.Gender = driver.Пол;
            this.Birthday = driver.ДатаРождения.Date;

            if (driver.Пользователь != null)
                this.User = new UserDto(driver.Пользователь);
        }
    }
}