/*SELECT all regions
*/

DROP PROC IF EXISTS spSelectPrinters;
GO 
CREATE PROC spSelectPrinters 
AS
SELECT Marke, Modell, Typ, Beschreibung, Druckbereich_Breite AS 'Breite Druckbereich', Druckbereich_Laenge AS 'Länge Druckbereich', Druckbereich_Hoehe AS 'Höhe Druckbereich'  FROM tbl_Drucker 
ORDER BY ID;
/*
EXEC spSelectPrinters;*/