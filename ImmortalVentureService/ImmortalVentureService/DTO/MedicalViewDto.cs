using ImmortalVentureService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImmortalVentureService.DTO
{
    public class MedicalViewDto
    {
        public string Hash { get; set; }
        public string VisualView { get; set; }
        public DriverDto Driver { get; set; }
        public DoctorDto Doctor { get; set; }
        public int? DriverId { get; set; }
        public int? DoctorId { get; set; }
        public DateTime Date { get; set; }
        public int? PressureTop { get; set; }
        public int? PressureBottom { get; set; }
        public decimal? Temperature { get; set; }
        public int? Pulse { get; set; }
        public string Comment { get; set; }
        public Заключение Result { get; set; }
        public string Complaint { get; set; }
        public decimal? Promille { get; set; }
        public MedicalViewType Type { get; set; }


        public MedicalViewDto()
        {
        }

        public MedicalViewDto(МедосмотрАвтоматический med)
        {
            this.Hash = med.ВнешнийХэш;
            this.Date = med.Дата.Date;
            this.Driver = new DriverDto(med.Водитель);
            //this.Doctor = new DoctorDto(med.Врач);
            //this.VisualView = med.ВизуальныйОсмотр;
            //this.Complaint = med.Жалобы;
            //this.Comment = med.Комментарий;
            this.PressureTop = med.ДавлениеВерхнее;
            this.PressureBottom = med.ДавлениеНижнее;
            this.Promille = med.ОпьянениеПромилле;
            this.Pulse = med.Пульс;
            this.Result = med.Заключение;
            this.Temperature = med.Температура;
            this.Type = MedicalViewType.Auto;
        }

        public MedicalViewDto(МедосмотрСВрачом med)
        {
            this.Hash = med.ВнешнийХэш;
            this.Date = med.Дата.Date;
            this.Driver = new DriverDto(med.Водитель);
            this.Doctor = new DoctorDto(med.Врач);
            this.VisualView = med.ВизуальныйОсмотр;
            this.Complaint = med.Жалобы;
            this.Comment = med.Комментарий;
            this.PressureTop = med.ДавлениеВерхнее;
            this.PressureBottom = med.ДавлениеНижнее;
            this.Promille = med.ОпьянениеПромилле;
            this.Pulse = med.Пульс;
            this.Result = med.Заключение;
            this.Temperature = med.Температура;
            this.Type = MedicalViewType.Doctor;
        }

    }


    public enum MedicalViewType
    {
        Auto,
        Doctor
    }



}