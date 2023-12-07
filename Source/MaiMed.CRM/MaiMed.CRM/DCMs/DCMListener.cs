using MaiMed.CRM.Models;
using MaiMed.CRM.Tools;
using Sagede.OfficeLine.Shared.Customizing;
using Sagede.OfficeLine.Wawi.BelegProxyEngine;
using System;

namespace MaiMed.CRM.DCMs
{
    public class DcmListener : IDcmCallback
    {
        public bool Entry(IDcmContext context)
        {

            Logger.TracelogVerbose($"{nameof(DcmListener)}:{nameof(Entry)} is called");

            try
            {

                switch (context.ListId)
                {
                    case DcmDefinitionManager.DcmListId.VKBelegProxyAfterSave:
                        VKBelegProxyAfterSave((DcmContextBelegProxyAfterSave)context); //Beispiel DCM!!
                        break;
                    case DcmDefinitionManager.DcmListId.VKBelegProxyBeforeSave:
                        VKBelegProxyBeforeSave((DcmContextBelegProxyBeforeSave)context);
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError("DCMListener.Entry", ex);
                return false;
            }
        }

        private void VKBelegProxyBeforeSave(DcmContextBelegProxyBeforeSave context)
        {

        }
        private void VKBelegProxyAfterSave(DcmContextBelegProxyAfterSave context)
        {
            //Beispiel DCM!!
            //var businessLogic = new MeinTollerBusinessLogicName(context.Mandant, SolutionServerHelper.ConfigReader.GetConfig<SolutionServerConfiguration>(context.Mandant));

            //businessLogic.MyLogic(context.Beleg);
        }
    }
}