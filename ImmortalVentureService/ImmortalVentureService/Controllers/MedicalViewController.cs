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
    public class MedicalViewController : ApiController
    {
        DBWork db = new DBWork();

        [HttpGet]
        [ActionName("GetMedicalView")]
        public MedicalViewDto GetMedicalView(int id, MedicalViewType type)
        {
            if (type == MedicalViewType.Auto)
            {
                var viewAuto = db.GetFromDatabase<МедосмотрАвтоматический>(x => x.Id == id).FirstOrDefault();
                if (viewAuto == null)
                    return null;

                return new MedicalViewDto(viewAuto);
            }
            else if (type == MedicalViewType.Doctor)
            {
                var viewDoctor = db.GetFromDatabase<МедосмотрСВрачом>(x => x.Id == id).FirstOrDefault();
                if (viewDoctor == null)
                    return null;

                return new MedicalViewDto(viewDoctor);
            }

            return null;
        }

        [HttpGet]
        [ActionName("GetMedicalViewByHash")]
        public MedicalViewDto GetMedicalViewByHash(string hash)
        {
            // просто поищем сначала одни, потом другие
            if (string.IsNullOrEmpty(hash))
                return null;

            var viewDoctor = db.GetFromDatabase<МедосмотрСВрачом>(x => x.ВнешнийХэш == hash).FirstOrDefault();

            if (viewDoctor != null)
            {
                return new MedicalViewDto(viewDoctor);
            }

            var viewAuto = db.GetFromDatabase<МедосмотрАвтоматический>(x => x.ВнешнийХэш == hash).FirstOrDefault();


            if (viewAuto == null)
                return null;

            return new MedicalViewDto(viewAuto);
        }


        [HttpPost]
        [ActionName("AddMedicalView")]
        public int AddMedicalView([FromBody] MedicalViewDto dto)
        {
            if (dto == null)
                return 0;

            if (dto.Type == MedicalViewType.Auto)
            {
                return AddАвтоматическийМедосмотр(dto);
            }
            else
            {
                return AddМедосмотрСВрачом(dto);
            }
        }

        private int AddМедосмотрСВрачом(MedicalViewDto dto)
        {
            МедосмотрСВрачом medDoctor = new МедосмотрСВрачом()
            {
                ВизуальныйОсмотр = dto.VisualView,
                ВнешнийХэш = dto.Hash,
                Водитель = db.GetFromDatabase<Водитель>(x => x.Id == dto.DriverId).FirstOrDefault(),
                Врач = db.GetFromDatabase<Врач>(x => x.Id == dto.DoctorId).FirstOrDefault(),
                ДавлениеВерхнее = dto.PressureTop ?? 0,
                ДавлениеНижнее = dto.PressureBottom ?? 0,
                Дата = dto.Date,
                Жалобы = dto.Complaint,
                Заключение = dto.Result,
                Комментарий = dto.Comment,
                ОпьянениеПромилле = dto.Promille ?? 0,
                Пульс = dto.Pulse ?? 0,
                Температура = dto.Temperature ?? 0
            };

            if (string.IsNullOrEmpty(medDoctor.ВнешнийХэш))
                medDoctor.ВнешнийХэш = HashHelper.GetHashForNewEntity(medDoctor);

            db.Insert(medDoctor);
            return medDoctor.Id;
        }

        private int AddАвтоматическийМедосмотр(MedicalViewDto dto)
        {
            МедосмотрАвтоматический medAuto = new МедосмотрАвтоматический()
            {
                ВнешнийХэш = dto.Hash,
                Водитель = db.GetFromDatabase<Водитель>(x => x.Id == dto.DriverId).FirstOrDefault(),
                ДавлениеВерхнее = dto.PressureTop ?? 0,
                ДавлениеНижнее = dto.PressureBottom ?? 0,
                Дата = dto.Date,
                Заключение = dto.Result,
                ОпьянениеПромилле = dto.Promille ?? 0,
                Пульс = dto.Pulse ?? 0,
                Температура = dto.Temperature ?? 0
            };

            if (string.IsNullOrEmpty(medAuto.ВнешнийХэш))
                medAuto.ВнешнийХэш = HashHelper.GetHashForNewEntity(medAuto);

            db.Insert(medAuto);
            return medAuto.Id;
        }
    }
}
