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
    public class DashboardeintragLoeschen : MacroProcessBase
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

                    Repository.DeleteDashboardId(base.Mandant, _Id);

                    cancelMessage = String.Empty;
                    return parameters;
                }

                cancelMessage = String.Empty;
                return parameters;
            }


            catch (Exception ex)
            {
                Logger.LogError("DashboardeintragLoeschen.Execute", ex);
                cancel = true;
                cancelMessage = ex.Message;
                return parameters;
            }
        }

        protected override string Name
        {
            get { return "DashboardeintragLoeschen"; }
        }

        protected override void Prepare()
        {
        }
    }
}