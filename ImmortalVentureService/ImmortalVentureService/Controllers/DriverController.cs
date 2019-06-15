using ImmortalVentureService.Classes;
using ImmortalVentureService.DTO;
using ImmortalVentureService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ImmortalVentureService.Controllers
{
    public class DriverController : ApiController
    {
        private DBWork db = new DBWork();

        [HttpGet]
        [ActionName("GetDriver")]
        public DriverDto GetDriver(int id)
        {
            var driver = db.GetFromDatabase<Водитель>(x => x.Id == id).FirstOrDefault();

            if (driver == null)
                return null;

            return new DriverDto(driver);
        }

        [HttpGet]
        [ActionName("GetDriverByHash")]
        public DriverDto GetDriverByHash(string hash)
        {
            if (string.IsNullOrEmpty(hash))
                return null;

            var driver = db.GetFromDatabase<Водитель>(x => x.ВнешнийХэш == hash).FirstOrDefault();

            if (driver == null)
                return null;

            return new DriverDto(driver);
        }


        [HttpPost]
        [ActionName("AddDriver")]
        public int AddDriver([FromBody] DriverDto dto)
        {
            if (dto == null)
                return 0;

            Водитель driver = new Водитель()
            {
                ВнешнийХэш = dto.Hash,
            };

            int? userId = dto.UserId;
            if (userId == null)
                userId = new UserController().AddUser(dto.User);

            driver.Пользователь = db.GetFromDatabase<Пользователь>(x => x.Id == userId).FirstOrDefault();

            if (string.IsNullOrEmpty(driver.ВнешнийХэш))
                driver.ВнешнийХэш = HashHelper.GetHashForNewEntity(driver);

            db.Insert(driver);
            return driver.Id;
        }


    }
}