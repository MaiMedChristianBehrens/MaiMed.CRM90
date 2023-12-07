USE [MaiMed_dev]
GO

INSERT INTO [dbo].[MaiMed_CRM_Dashboard]
           ([Mandant]
           ,[KundenNr]
           ,[Matchcode]
           ,[Name1]
           ,[Name2]
           ,[Strasse]
           ,[PLZ]
           ,[Ort]
           ,[Land]
           ,[Telefon]
           ,[Email]
           ,[Adresse]
           ,[Ansprechpartner]
           ,[MatchcodeAP]
           ,[TelefonAP]
           ,[EmailAP]
           ,[Bearbeiter]
           ,[Ausfuehrer]
           ,[Stufe]
           ,[Status]
           ,[Betreff]
           ,[Erfassungsdatum]
           ,[Termin]
           ,[Memo]
           ,[Vorgangsart])
    
	SELECT 


	1, k.Kto, a.Matchcode, a.Name1, a.Name2, a.LieferStrasse, a.LieferPLZ, a.LieferOrt, a.LieferLand, a.Telefon, a.EMail, P.Herkunft, P.AnsprechpartnerID, P.Ansprechpartner, 
	ap.Telefon, ap.EMail, P.Bearbeiter, P.Ausfuehrer, P.Stufe, P.Status, P.Betreff, P.Erfassungsdatum, P.Wiedervorlagedatum, P.Text, P.Vorgangsart

	FROM BCSPjmProjektePositionen p
	INNER JOIN KHKAdressen a ON p.Herkunft = a.Adresse
	INNER JOIN KHKKontokorrent k ON a.Adresse = k.Adresse	
	INNER JOIN KHKAnsprechpartner ap ON P.AnsprechpartnerID = ap.Nummer
