using MaiMed.CRM.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaiMed.CRM.SQL;
using Sagede.Shared.RealTimeData.Common;
using Sagede.OfficeLine.Shared.RealTimeData.MacroProcess;
using Sagede.OfficeLine.Engine;
using Sagede.Core.Tools;
using Sagede.Shared.ServiceModel.Gateway.Mail;

namespace MaiMed.CRM.Macros
{
    public class DashboardeintragNeu : MacroProcessBase
    {
        private int _belID;
        private int _Id;


        protected override NamedParameters Execute(NamedParameters parameters, ref bool cancel, ref string cancelMessage)
        {
            try
            {
                Mandant mandant = this.Mandant;

                if (parameters != null)
                {
                    _Id = ConversionHelper.ToInt32(parameters.TryGetItem("_Id").Value);
                    _Id = base.Mandant.MainDevice.GetTan("MaiMed_CRM_Dashboard", mandant.Id);
                    Repository.CreateNewDashboardId(base.Mandant, _Id);

                    parameters.SetParameter("_Id", _Id);
                    cancelMessage = String.Empty;
                    return parameters;
                }

                cancelMessage = String.Empty;
                return parameters;
            }


            catch (Exception ex)
            {
                Logger.LogError("DashboardeintragNeu.Execute", ex);
                cancel = true;
                cancelMessage = ex.Message;
                return parameters;
            }
        }

        protected override string Name
        {
            get { return "DashboardeintragNeu"; }
        }

        protected override void Prepare()
        {
        }
    }
}