using MaiMed.CRM;
using MaiMed.CRM.SQL;
using Sagede.Core.Tools;
using Sagede.OfficeLine.Shared.RealTimeData;
using Sagede.OfficeLine.Shared.RealTimeData.MacroProcess;
using Sagede.OfficeLine.Wawi.BelegProxyEngine;
using Sagede.Shared.RealTimeData.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Sagede.OfficeLine.Shared;
using MaiMed.CRM.Tools;

namespace MaiMed.CRM
{
    public class KundendatenErmitteln : MacroProcessBase
    {
        protected override string Name => nameof(KundendatenErmitteln);

        protected override NamedParameters Execute(NamedParameters parameters, ref bool cancel, ref string cancelMessage)
        {
            Logger.TracelogVerbose($"{nameof(KundendatenErmitteln)}:Execute is called");

            string fstr = "Es ist ein unbekannter Fehler aufgetreten";
            try
            {
                string kundenNr = ConversionHelper.ToString(parameters.TryGetItem("_Kto").Value);
                var adresse = Repository.GetAdresseFromKto(base.Mandant, kundenNr);

                //string matchcode = ConversionHelper.ToString(parameters.TryGetItem("_Matchcode").Value);
                //string name1 = ConversionHelper.ToString(parameters.TryGetItem("_Name1").Value);
                //string name2 = ConversionHelper.ToString(parameters.TryGetItem("_Name2").Value);
                //string lieferStrasse = ConversionHelper.ToString(parameters.TryGetItem("_LieferStrasse").Value);
                //string lieferPLZ = ConversionHelper.ToString(parameters.TryGetItem("_LieferPLZ").Value);
                //string lieferOrt = ConversionHelper.ToString(parameters.TryGetItem("_LieferOrt").Value);
                //string lieferLand = ConversionHelper.ToString(parameters.TryGetItem("_LieferLand").Value);
                //string telefon = ConversionHelper.ToString(parameters.TryGetItem("_Telefon").Value);
                //string eMail = ConversionHelper.ToString(parameters.TryGetItem("_EMail").Value);

                var matchcode = string.Empty;
                var name1 = string.Empty;
                var name2 = string.Empty;
                var lieferStrasse = string.Empty;
                var lieferPLZ = string.Empty;
                var lieferOrt = string.Empty;
                var lieferLand = string.Empty;
                var telefon = string.Empty;
                var eMail = string.Empty;


                Repository.GetAdressdaten(base.Mandant, adresse, ref matchcode, ref name1, ref name2, ref lieferStrasse, ref lieferPLZ, ref lieferOrt, ref lieferLand, ref telefon, ref eMail);
                parameters.SetParameter("_Adresse", adresse);
                parameters.SetParameter("_Matchcode", matchcode);
                parameters.SetParameter("_Name1", name1);
                parameters.SetParameter("_Name2", name2);
                parameters.SetParameter("_LieferStrasse", lieferStrasse);
                parameters.SetParameter("_LieferPLZ", lieferPLZ);
                parameters.SetParameter("_LieferOrt", lieferOrt);
                parameters.SetParameter("_LieferLand", lieferLand);
                parameters.SetParameter("_Telefon", telefon);
                parameters.SetParameter("_EMail", eMail);

                cancel = false;
                cancelMessage = String.Empty;
                return parameters;
            }
            catch (Exception ex)
            {
                Logger.LogError($"{nameof(KundendatenErmitteln)}.Execute", ex);
                parameters.SetParameter("_SuccessMessage", fstr + " - " + ex.Message);
                cancel = true;
                cancelMessage = ex.Message;
                return parameters;
            }
        }

        protected override void Prepare()
        {
        }
    }
}