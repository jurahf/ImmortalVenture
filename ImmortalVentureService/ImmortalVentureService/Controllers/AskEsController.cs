using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ImmortalVentureService.Controllers
{
    public class AskEsController : ApiController
    {
        [HttpPost]
        [ActionName("AdaptValues")]
        public object AdaptValues([FromBody] DataFromDevices devicesData)
        {
            EsParameters parameters = new EsParameters();

            if (devicesData.Temperature != null)
            {
                parameters.VarValues.Add(new EsVariables() { Variable = "ТемператураЗначением", Value = devicesData.GetTemperatureRange().ToString() });
            }

            // TODO: преобразовать параметры
            // TODO: отправить запрос в ЭС POST-запросом

            throw new NotImplementedException();
        }

    }

    public class EsParameters
    {
        //{ FileName: "MedicalView.es", Goal: "Состояние", VarValues: [ { Variable: "ТемператураЗначением", Value: "от36и5до37и1" }, { Variable: "ПульсЗначением", Value: "от50до85" }, { Variable: "ДавлениеЗначением", Value: "от140до150" } ] }
        public string FileName => "MedicalView.es";

        public string Goal => "Состояние";

        public List<EsVariables> VarValues { get; set; } = new List<EsVariables>();
    }


    public class EsVariables
    {
        public string Variable { get; set; }
        public string Value { get; set; }
    }

    public class DataFromDevices
    {
        public decimal? Temperature { get; set; }
        public int? PressureTop { get; set; }
        public int? Pulse { get; set; }

        public TemperatureRange GetTemperatureRange()
        {
            if (Temperature < 36.5m)
                return TemperatureRange.меньше36и5;
            else if (Temperature > 37.1m)
                return TemperatureRange.больше37и1;
            else
                return TemperatureRange.от36и5до37и1;
        }
    }

    public enum TemperatureRange
    {
        меньше36и5,
        от36и5до37и1,
        больше37и1
    }

    public enum PulseRange
    {
        меньше50,
        от50до85,
        от85до95,
        больше95
    }

    public enum PressureTopRange
    {
        меньше100,
        от100до140,
        от140до150,
        больше150
    }

}
