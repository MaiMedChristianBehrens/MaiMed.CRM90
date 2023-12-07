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
    public class AnsprechpartnerdatenErmitteln : MacroProcessBase
    {
        protected override string Name => nameof(AnsprechpartnerdatenErmitteln);

        protected override NamedParameters Execute(NamedParameters parameters, ref bool cancel, ref string cancelMessage)
        {
            Logger.TracelogVerbose($"{nameof(AnsprechpartnerdatenErmitteln)}:Execute is called");

            string fstr = "Es ist ein unbekannter Fehler aufgetreten";
            try
            {
                int apNr = ConversionHelper.ToInt32(parameters.TryGetItem("_APNr").Value);
                string ansprechpartner = ConversionHelper.ToString(parameters.TryGetItem("_MatchcodeAP").Value);
                string telefon = ConversionHelper.ToString(parameters.TryGetItem("_TelefonAP").Value);
                string eMail = ConversionHelper.ToString(parameters.TryGetItem("_EMailAP").Value);

                Repository.GetAnsprechpartnerDaten(base.Mandant, apNr, ref ansprechpartner, ref telefon, ref eMail);

                parameters.SetParameter("_MatchcodeAP", ansprechpartner);
                parameters.SetParameter("_TelefonAP", telefon);
                parameters.SetParameter("_EMailAP", eMail);

                cancel = false;
                cancelMessage = String.Empty;
                return parameters;
            }
            catch (Exception ex)
            {
                Logger.LogError($"{nameof(AnsprechpartnerdatenErmitteln)}.Execute", ex);
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