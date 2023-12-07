using MaiMed.CRM.Tools;
using Sagede.Core.Tools;
using Sagede.OfficeLine.Data;
using Sagede.OfficeLine.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sagede.Shared.RealTimeData.Common.Utilities;
using MaiMed.CRM.Tools;
using Sagede.OfficeLine.Data.Entities.Main;

namespace MaiMed.CRM.SQL
{
    internal class Repository
    {
        internal static void GetAdressdaten(Mandant mandant, int adresse, ref string matchcode, ref string name1, ref string name2, ref string lieferStrasse, ref string lieferPLZ, ref string lieferOrt, ref string lieferLand, ref string telefon, ref string email)
        {
            var cmd = mandant.MainDevice.GenericConnection.CreateSqlStringCommand(
                $"SELECT Matchcode, Name1, Name2, LieferStrasse, LieferPLZ, LieferOrt, LieferLand, Telefon, Email FROM KHKAdressen WHERE Mandant = @mandant AND Adresse = @adresse ");
            cmd.AppendInParameter("Mandant", typeof(short), mandant.Id);
            cmd.AppendInParameter("adresse", typeof(int), adresse);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    matchcode = ConversionHelper.ToString(reader.GetString("Matchcode"));
                    name1 = ConversionHelper.ToString(reader.GetString("Name1"));
                    name2 = ConversionHelper.ToString(reader.GetString("Name2"));
                    lieferStrasse = ConversionHelper.ToString(reader.GetString("LieferStrasse"));
                    lieferPLZ = ConversionHelper.ToString(reader.GetString("LieferPLZ"));
                    lieferOrt = ConversionHelper.ToString(reader.GetString("LieferOrt"));
                    lieferLand = ConversionHelper.ToString(reader.GetString("LieferLand"));
                    telefon = ConversionHelper.ToString(reader.GetString("Telefon"));
                    email = ConversionHelper.ToString(reader.GetString("Email"));
                }
            }
        }

        internal static int GetAdresseFromKto(Mandant mandant, string kto)
        {
            var adresse = 0;

            var cmd = mandant.MainDevice.GenericConnection.CreateSqlStringCommand(
                $"SELECT Adresse FROM KHKKontokorrent WHERE Mandant = @mandant AND Kto = @kto ");
            cmd.AppendInParameter("Mandant", typeof(short), mandant.Id);
            cmd.AppendInParameter("Kto", typeof(string), kto);


            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    adresse = ConversionHelper.ToInt32(reader.GetInt32("Adresse"));
                }
            }

            return adresse;
        }

        internal static void GetAnsprechpartnerDaten(Mandant mandant, int apNr, ref string ansprechpartner, ref string telefon, ref string email)
        {
            var cmd = mandant.MainDevice.GenericConnection.CreateSqlStringCommand(
                $"SELECT Ansprechpartner, Telefon, EMail FROM KHKAnsprechpartner WHERE Mandant = @mandant AND Nummer = @Nummer");
            cmd.AppendInParameter("Mandant", typeof(short), mandant.Id);
            cmd.AppendInParameter("Nummer", typeof(int), apNr);


            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    ansprechpartner = ConversionHelper.ToString(reader.GetString("Ansprechpartner"));
                    telefon = ConversionHelper.ToString(reader.GetString("Telefon"));
                    email = ConversionHelper.ToString(reader.GetString("EMail"));
                }
            }
        }

        public static void CreateNewDashboardId(Mandant mandant, int id)
        {
            var cmd = mandant.MainDevice.GenericConnection.CreateSqlStringCommand(
                $"INSERT INTO [dbo].[MaiMed_CRM_Dashboard] (Mandant, ID) SELECT {mandant.Id}, {id}");
            cmd.ExecuteNonQuery();
        }

        public static void DeleteDashboardId(Mandant mandant, int id)
        {
            var cmd = mandant.MainDevice.GenericConnection.CreateSqlStringCommand(
                $"DELETE FROM [dbo].[MaiMed_CRM_Dashboard] WHERE Mandant = {mandant.Id} AND ID = {id}");
            cmd.ExecuteNonQuery();
        }
    }
}