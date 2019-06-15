using ImmortalVentureService.Classes;
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
    public class DoctorController : ApiController
    {
        private DBWork db = new DBWork();

        [HttpGet]
        [ActionName("GetDoctor")]
        public DoctorDto GetDoctor(int id)
        {
            var doc = db.GetFromDatabase<Врач>(x => x.Id == id).FirstOrDefault();

            if (doc == null)
                return null;

            return new DoctorDto(doc);
        }

        [HttpGet]
        [ActionName("GetDoctorByHash")]
        public DoctorDto GetDoctorByHash(string hash)
        {
            if (string.IsNullOrEmpty(hash))
                return null;

            var doc = db.GetFromDatabase<Врач>(x => x.ВнешнийХэш == hash).FirstOrDefault();

            if (doc == null)
                return null;

            return new DoctorDto(doc);
        }


        [HttpPost]
        [ActionName("AddDoctor")]
        public int AddDoctor([FromBody] DoctorDto dto)
        {
            if (dto == null)
                return 0;

            Врач doc = new Врач()
            {
                ВнешнийХэш = dto.Hash,                
            };

            int? userId = dto.UserId;
            if (userId == null)
                userId = new UserController().AddUser(dto.User);

            doc.Пользователь = db.GetFromDatabase<Пользователь>(x => x.Id == userId).FirstOrDefault();

            if (string.IsNullOrEmpty(doc.ВнешнийХэш))
                doc.ВнешнийХэш = HashHelper.GetHashForNewEntity(doc);

            db.Insert(doc);
            return doc.Id;
        }


    }
}
