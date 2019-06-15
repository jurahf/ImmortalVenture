using ImmortalVentureService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImmortalVentureService.DTO
{
    public class DoctorDto
    {
        public string Hash { get; set; }
        public int? UserId { get; set; }
        public UserDto User { get; set; }

        public DoctorDto(Врач doc)
        {
            if (doc == null)
                return;

            this.Hash = doc.ВнешнийХэш;
            this.UserId = doc.Пользователь?.Id;

            if (doc.Пользователь != null)
                this.User = new UserDto(doc.Пользователь);
        }

    }


}