using ImmortalVentureService.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace ImmortalVentureService.Controllers
{
    public class AskEsController : ApiController
    {
        [HttpPost]
        [ActionName("AdaptValues")]
        public ConsultResultDto AdaptValues([FromBody] DataFromDevices devicesData)
        {
            EsParameters parameters = new EsParameters();

            if (devicesData.Temperature != null)
            {
                parameters.VarValues.Add(new EsVariables() { Variable = "ТемператураЗначением", Value = devicesData.GetTemperatureRange().ToString() });
            }

            if (devicesData.Pulse != null)
            {
                parameters.VarValues.Add(new EsVariables() { Variable = "ПульсЗначением", Value = devicesData.GetPulseRange().ToString() });
            }

            if (devicesData.PressureTop != null)
            {
                parameters.VarValues.Add(new EsVariables() { Variable = "ДавлениеЗначением", Value = devicesData.GetPressureRange().ToString() });
            }

            // отправляем POST-запрос в экспертную систему
            ConsultResultDto result = null;
            if (parameters.VarValues.Any())
            {
                string json = new JavaScriptSerializer().Serialize(parameters);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://immortalventurees.azurewebsites.net");

                HttpResponseMessage response = client.PostAsync("/api/expertSystem/GoConsult", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsAsync<ConsultResultDto>().Result;
                }
            }

            return result;
        }



    }

    public class EsParameters
    {
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

        public PressureTopRange GetPressureRange()
        {
            if (PressureTop < 100)
                return PressureTopRange.меньше100;
            else if (PressureTop < 140)
                return PressureTopRange.от100до140;
            else if (PressureTop < 150)
                return PressureTopRange.от140до150;
            else
                return PressureTopRange.больше150;
        }

        public PulseRange GetPulseRange()
        {
            if (Pulse < 50)
                return PulseRange.меньше50;
            else if (Pulse < 85)
                return PulseRange.от50до85;
            else if (Pulse < 95)
                return PulseRange.от85до95;
            else
                return PulseRange.больше95;
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
